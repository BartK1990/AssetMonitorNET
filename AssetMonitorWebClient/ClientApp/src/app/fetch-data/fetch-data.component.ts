import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  private intervalId: any;
  private subs: Subscription;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.updateData();
  }

  ngOnInit() {
    this.intervalId = setInterval(() => {
      this.updateData();
    }, 2000);
  }

  ngOnDestroy() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  updateData() {
    let lastSubsClosed = true;
    if (this.subs != null) {
      lastSubsClosed = this.subs.closed;
    }
    console.log(lastSubsClosed);
    if (lastSubsClosed) {
      this.subs = this.http.get<WeatherForecast[]>(this.baseUrl + 'weatherforecast').subscribe(
        result => { this.forecasts = result; },
        error => console.error(error));
    }
  }

}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

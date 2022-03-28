export interface Tag {
    sharedTagId: number;
    tagname: string;
    dataType: string;
    value?: any;
    inAlarm: boolean;
    rangeMax?: number;
    rangeMin?: number;
}


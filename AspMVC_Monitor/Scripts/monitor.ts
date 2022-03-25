//import { SharedTagSet } from "./Monitor/SharedTagSet";

const SharedTagTableId: string = 'tableAssets';
var SharedTagNumberOfColumns: number = 0;
var TagSetId: number = null;
var SharedTagSets: SharedTagSet[] = null;
var GetAssetsLiveDataInterval: number = null;

document.addEventListener("DOMContentLoaded", function (event) {
    init();
});

function init() {
    $("#sessionValue").text("00.00.0000 00:00:00");
    timeNowCall();
    timeNowTimer();

    var tableAssets: HTMLTableElement = <HTMLTableElement>document.getElementById(SharedTagTableId);
    SharedTagNumberOfColumns = tableAssets.tHead.children[0].childElementCount;
}
function timeNowTimer() {
    setInterval(function () {
        $.ajax({
            type: 'post',
            url: 'GetServerTime',
            success: function (result) {
                $("#sessionValue").text(result.data);
            }
        })
    }, 1000);
}
function timeNowCall() {
    $.ajax({
        type: 'post',
        url: 'GetServerTime',
        success: function (result) {
            $("#sessionValue").text(result.data);
        }
    })
}

function GetSharedTagColumns(tagSetId: number) {
        $.ajax({
            type: 'post',
            url: 'GetSharedTagColumns?tagSetId=' + tagSetId,
            dataType: 'json',
            success: function (data) {
                var tableAssets: HTMLTableElement = <HTMLTableElement>document.getElementById(SharedTagTableId);
                var rows = tableAssets.rows;

                for (var j = 0; j < rows.length; j++) {
                    var columnsLength = rows[j].cells.length;
                    for (var i = 0; i < columnsLength; i++) {
                        if (i >= SharedTagNumberOfColumns) {
                            rows[j].deleteCell(SharedTagNumberOfColumns);
                        }
                    }
                }

                var sharedTagSets: SharedTagSet[] = data;

                if (sharedTagSets.length === 0) {
                    return;
                }

                TagSetId = tagSetId;
                SharedTagSets = sharedTagSets;

                $.each(sharedTagSets, function (i, tagSet) {
                    var th = document.createElement('th');
                    th.innerHTML = tagSet.columnName;
                    tableAssets.tHead.children[0].appendChild(th);
                });

                GetAssetsLiveData();
            },
        });
}

function GetAssetsLiveData() {
    if (GetAssetsLiveDataInterval != null) {
        clearInterval(GetAssetsLiveDataInterval);
    }
    if ((TagSetId === null) || (SharedTagSets === null)) {
        return;
    }

    // Build table
    $.ajax({
        type: 'post',
        url: 'GetAssetsLiveData?tagSetId=' + TagSetId,
        dataType: 'json',
        success: function (data) {
            var assetsData: AssetData[] = data;
            var tableAssets: HTMLTableElement = <HTMLTableElement>document.getElementById(SharedTagTableId);
            var tableAssetsBody = tableAssets.tBodies[0];

            tableAssetsBody.innerHTML = '';
            $.each(assetsData, function (i, assetData) {
                var newRow = tableAssetsBody.insertRow();
                var cellName = newRow.insertCell();
                cellName.appendChild(document.createTextNode(assetData.name));
                var cellIp = newRow.insertCell();
                cellIp.appendChild(document.createTextNode(assetData.ipAddress));
                var cellInAlarm = newRow.insertCell();
                cellInAlarm.appendChild(document.createTextNode(String(assetData.inAlarm)));

                for (var i = 0; i < SharedTagSets.length; i++) {
                    var cellTag = newRow.insertCell();
                    for (var j = 0; j < assetData.tags.length; j++) {
                        if (assetData.tags[j].sharedTagId === SharedTagSets[i].id)
                        {
                            var tagValue = assetData.tags[j];
                            break;
                        }
                    }
                    var tagVal = tagValue.value;
                    if (tagVal != null) {
                        if (tagValue.dataType == 'Float') {
                            tagVal = parseFloat(tagVal.toFixed(2));
                        }
                        cellTag.appendChild(document.createTextNode(String(tagVal)));
                    }
                }
            });
        },
    });

    // Update data
    GetAssetsLiveDataInterval = setInterval(function () {
        $.ajax({
            type: 'post',
            url: 'GetAssetsLiveData?tagSetId=' + TagSetId,
            dataType: 'json',
            success: function (data) {
                var assetsData: AssetData[] = data;
                $.each(assetsData, function (i, assetData) {


                });
                //$.each(data, function (i, item) {
                //    const ps = document.getElementById(item.name + "_PingState");
                //    ps.innerHTML = '';
                //    const span = document.createElement('span');
                //    span.classList.add('dot');
                //    var pingState = String(item.pingState).toLowerCase() === "true" ? true : false;
                //    if (pingState) {
                //        span.classList.add('dotBackgroundGreen');
                //        ps.appendChild(span);
                //    }
                //    else {
                //        span.classList.add('dotBackgroundRed');
                //        ps.appendChild(span);
                //    }
                //    document.getElementById(item.name + "_PingResponseTime").innerHTML = item.pingResponseTime + "ms";

                //    const cpu = document.getElementById(item.name + "_CpuUsage");
                //    cpu.getElementsByClassName('valuebar-value-wrapper')[0].getElementsByClassName('valuebar-value')[0].innerHTML = item.cpuUsage + "%";
                //    cpu.getElementsByClassName('progress')[0].getElementsByClassName('progress-bar')[0].style.width = item.cpuUsage + "%";

                //    const memory = document.getElementById(item.name + "_MemoryAvailable");
                //    memory.getElementsByClassName('valuebar-value-wrapper')[0].getElementsByClassName('valuebar-value')[0].innerHTML = item.memoryAvailable + "/" + item.memoryTotal;
                //    memory.getElementsByClassName('progress')[0].getElementsByClassName('progress-bar')[0].style.width = item.memoryUsage + "%";
                //});
            },
        });
    }, 10000); 
}

interface SharedTagSet {
    id: number;
    columnName: string;
}

interface Tag {
    sharedTagId: number;
    tagname: string;
    dataType: string;
    value?: any;
    inAlarm: boolean;
    rangeMax?: number;
    rangeMin?: number;
}

interface AssetData {
    name: string;
    ipAddress: string;
    inAlarm: boolean;
    tags: Tag[];
}

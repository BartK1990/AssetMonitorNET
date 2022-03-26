//import { SharedTagSet } from "./Monitor/SharedTagSet";
const SharedTagTableId = 'tableAssets';
var SharedTagInitNumberOfColumns = 0;
var TagSetId = null;
var SharedTagSets = null;
var SharedTagTableRows = null;
var GetAssetsLiveDataInterval = null;
document.addEventListener("DOMContentLoaded", function (event) {
    init();
});
function init() {
    $("#sessionValue").text("00.00.0000 00:00:00");
    timeNowCall();
    timeNowTimer();
    var tableAssets = document.getElementById(SharedTagTableId);
    SharedTagInitNumberOfColumns = tableAssets.tHead.children[0].childElementCount;
}
function timeNowTimer() {
    setInterval(function () {
        $.ajax({
            type: 'post',
            url: 'GetServerTime',
            success: function (result) {
                $("#sessionValue").text(result.data);
            }
        });
    }, 1000);
}
function timeNowCall() {
    $.ajax({
        type: 'post',
        url: 'GetServerTime',
        success: function (result) {
            $("#sessionValue").text(result.data);
        }
    });
}
function GetSharedTagColumns(tagSetId) {
    $.ajax({
        type: 'post',
        url: 'GetSharedTagColumns?tagSetId=' + tagSetId,
        dataType: 'json',
        success: function (data) {
            var tableAssets = document.getElementById(SharedTagTableId);
            var rows = tableAssets.rows;
            for (var j = 0; j < rows.length; j++) {
                var columnsLength = rows[j].cells.length;
                for (var i = 0; i < columnsLength; i++) {
                    if (i >= SharedTagInitNumberOfColumns) {
                        rows[j].deleteCell(SharedTagInitNumberOfColumns);
                    }
                }
            }
            var sharedTagSets = data;
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
            var assetsData = data;
            var tableAssets = document.getElementById(SharedTagTableId);
            var tableAssetsBody = tableAssets.tBodies[0];
            tableAssetsBody.innerHTML = '';
            SharedTagTableRows = new Map();
            $.each(assetsData, function (i, assetData) {
                var newRow = tableAssetsBody.insertRow();
                SharedTagTableRows.set(assetData.id, newRow);
                var cellName = newRow.insertCell();
                cellName.classList.add('align-middle');
                cellName.appendChild(document.createTextNode(assetData.name));
                var cellIp = newRow.insertCell();
                cellIp.classList.add('align-middle');
                cellIp.appendChild(document.createTextNode(assetData.ipAddress));
                var cellInAlarm = newRow.insertCell();
                cellInAlarm.classList.add('align-middle');
                DotForBoolean(cellInAlarm, assetData.inAlarm);
                for (var i = 0; i < SharedTagSets.length; i++) {
                    var cellTag = newRow.insertCell();
                    cellTag.classList.add('align-middle');
                    for (var j = 0; j < assetData.tags.length; j++) {
                        if (assetData.tags[j].sharedTagId == SharedTagSets[i].id) {
                            var tagValue = assetData.tags[j];
                            break;
                        }
                    }
                    var tagVal = tagValue.value;
                    if (tagVal != null) {
                        // Put value to cell
                        if (tagValue.dataType == 'Float' ||
                            tagValue.dataType == 'Double') {
                            tagVal = parseFloat(tagVal.toFixed(2));
                        }
                        if (tagValue.rangeMax != null && tagValue.rangeMin != null) {
                            ValueBarForNumber(cellTag, tagVal, tagValue.rangeMax, tagValue.rangeMin);
                            continue;
                        }
                        if (tagValue.dataType == 'Boolean') {
                            var bool = tagVal;
                            DotForBoolean(cellTag, bool);
                            continue;
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
                var assetsData = data;
                $.each(assetsData, function (i, assetData) {
                    var row = SharedTagTableRows.get(assetData.id);
                    DotForBoolean(row.cells[2], assetData.inAlarm);
                    for (var i = 0; i < SharedTagSets.length; i++) {
                        var cellTag = row.cells[i + SharedTagInitNumberOfColumns];
                        for (var j = 0; j < assetData.tags.length; j++) {
                            if (assetData.tags[j].sharedTagId == SharedTagSets[i].id) {
                                var tagValue = assetData.tags[j];
                                break;
                            }
                        }
                        var tagVal = tagValue.value;
                        if (tagVal != null) {
                            // Put value to cell
                            if (tagValue.dataType == 'Float' ||
                                tagValue.dataType == 'Double') {
                                tagVal = parseFloat(tagVal.toFixed(2));
                            }
                            if (tagValue.rangeMax != null && tagValue.rangeMin != null) {
                                ValueBarForNumberUpdate(cellTag, tagVal, tagValue.rangeMax, tagValue.rangeMin);
                                continue;
                            }
                            if (tagValue.dataType == 'Boolean') {
                                var bool = tagVal;
                                DotForBoolean(cellTag, bool);
                                continue;
                            }
                            cellTag.innerHTML = '';
                            cellTag.appendChild(document.createTextNode(String(tagVal)));
                        }
                    }
                });
            },
        });
    }, 10000);
}
function DotForBoolean(cell, state) {
    if (cell == null) {
        return;
    }
    cell.innerHTML = '';
    var div = document.createElement('div');
    div.classList.add('d-flex');
    div.classList.add('justify-content-center');
    div.classList.add('align-items-center');
    var span = document.createElement('span');
    span.classList.add('dot');
    var spanInner = document.createElement('span');
    spanInner.classList.add('dotTextCenterDiv');
    spanInner.innerHTML = 'F';
    cell.appendChild(div);
    div.appendChild(span);
    span.appendChild(spanInner);
    if (state == null) {
        return;
    }
    if (state) {
        span.classList.add('dotBackgroundTrue');
        spanInner.innerHTML = 'T';
    }
}
function ValueBarForNumber(cell, value, rangeMax, rangeMin) {
    if (cell == null) {
        return;
    }
    cell.innerHTML = '';
    if ((rangeMax <= rangeMin) || (value < rangeMin) || (value > rangeMax)) {
        cell.appendChild(document.createTextNode(String(value)));
        return;
    }
    var divOuter = document.createElement('div');
    divOuter.classList.add('valuebar');
    var divInner1 = document.createElement('div');
    divInner1.classList.add('valuebar-value-wrapper');
    var divInner1Inner = document.createElement('div');
    divInner1Inner.classList.add('valuebar-value');
    var divInner2 = document.createElement('div');
    divInner2.classList.add('valuebar-progress');
    divInner2.classList.add('progress');
    var divInner2Inner = document.createElement('div');
    divInner2Inner.classList.add('progress-bar');
    divInner2Inner.classList.add('bg-site-orange');
    var fillProcent = ((value - rangeMin) / (rangeMax - rangeMin)) * 100;
    divInner2Inner.style.width = fillProcent + '%';
    cell.appendChild(divOuter);
    divOuter.appendChild(divInner1);
    divOuter.appendChild(divInner2);
    divInner1.appendChild(divInner1Inner);
    divInner2.appendChild(divInner2Inner);
    divInner1Inner.appendChild(document.createTextNode(String(value)));
}
function ValueBarForNumberUpdate(cell, value, rangeMax, rangeMin) {
    if (cell == null) {
        return;
    }
    if ((rangeMax <= rangeMin) || (value < rangeMin) || (value > rangeMax)) {
        cell.innerHTML = '';
        cell.appendChild(document.createTextNode(String(value)));
        return;
    }
    var valueBarValue = cell.getElementsByClassName('valuebar-value')[0];
    var valueBarProgress = cell.getElementsByClassName('progress-bar')[0];
    var fillProcent = ((value - rangeMin) / (rangeMax - rangeMin)) * 100;
    valueBarProgress.style.width = fillProcent + '%';
    valueBarValue.innerHTML = String(value);
}
//# sourceMappingURL=monitor.js.map
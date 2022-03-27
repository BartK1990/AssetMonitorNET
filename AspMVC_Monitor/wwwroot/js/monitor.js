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
    GetAssetsLiveData();
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
    var url;
    if (TagSetId != null) {
        url = 'GetAssetsLiveData?tagSetId=' + TagSetId;
    }
    else {
        url = 'GetAssetsLiveData';
    }
    // Build table
    $.ajax({
        type: 'post',
        url: url,
        dataType: 'json',
        success: function (data) {
            var assets = data;
            AssetsStatusBarUpdate(assets.okCnt, assets.inAlarmCnt);
            var tableAssets = document.getElementById(SharedTagTableId);
            var tableAssetsBody = tableAssets.tBodies[0];
            tableAssetsBody.innerHTML = '';
            SharedTagTableRows = new Map();
            $.each(assets.assets, function (i, assetData) {
                var newRow = tableAssetsBody.insertRow();
                SharedTagTableRows.set(assetData.id, newRow);
                var cellName = newRow.insertCell();
                AssetsTablePrepareCell(cellName);
                AssetsTableGetValueElement(cellName).appendChild(document.createTextNode(assetData.name));
                var cellIp = newRow.insertCell();
                AssetsTablePrepareCell(cellIp);
                AssetsTableGetValueElement(cellIp).appendChild(document.createTextNode(assetData.ipAddress));
                var cellInAlarm = newRow.insertCell();
                AssetsTablePrepareCell(cellInAlarm);
                AssetTagBackgroundStatusUpdate(AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm, assetData.inAlarm);
                DotForBoolean(AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);
                if (SharedTagSets == null) {
                    return;
                }
                for (var i = 0; i < SharedTagSets.length; i++) {
                    var cellTag = newRow.insertCell();
                    AssetsTablePrepareCell(cellTag);
                    for (var j = 0; j < assetData.tags.length; j++) {
                        if (assetData.tags[j].sharedTagId == SharedTagSets[i].id) {
                            var tagValue = assetData.tags[j];
                            break;
                        }
                    }
                    var tagVal = tagValue.value;
                    AssetTagBackgroundStatusUpdate(AssetsTableGetValueElement(cellTag), tagVal, tagValue.inAlarm);
                    if (tagVal == null) {
                        tagVal = 0;
                    }
                    // Put value to cell
                    if (tagValue.dataType == 'Float' ||
                        tagValue.dataType == 'Double') {
                        tagVal = parseFloat(tagVal.toFixed(2));
                    }
                    if (tagValue.rangeMax != null && tagValue.rangeMin != null) {
                        ValueBarForNumber(AssetsTableGetValueElement(cellTag), tagVal, tagValue.rangeMax, tagValue.rangeMin);
                        continue;
                    }
                    if (tagValue.dataType == 'Boolean') {
                        var bool = tagVal;
                        DotForBoolean(AssetsTableGetValueElement(cellTag), bool);
                        continue;
                    }
                    AssetsTableGetValueElement(cellTag).appendChild(document.createTextNode(String(tagVal)));
                }
            });
        },
    });
    // Update data
    GetAssetsLiveDataInterval = setInterval(function () {
        $.ajax({
            type: 'post',
            url: url,
            dataType: 'json',
            success: function (data) {
                var assets = data;
                AssetsStatusBarUpdate(assets.okCnt, assets.inAlarmCnt);
                $.each(assets.assets, function (i, assetData) {
                    var row = SharedTagTableRows.get(assetData.id);
                    var cellInAlarm = row.cells[2];
                    AssetTagBackgroundStatusUpdate(AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm, assetData.inAlarm);
                    DotForBoolean(AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);
                    if (SharedTagSets == null) {
                        return;
                    }
                    for (var i = 0; i < SharedTagSets.length; i++) {
                        var cellTag = row.cells[i + SharedTagInitNumberOfColumns];
                        for (var j = 0; j < assetData.tags.length; j++) {
                            if (assetData.tags[j].sharedTagId == SharedTagSets[i].id) {
                                var tagValue = assetData.tags[j];
                                break;
                            }
                        }
                        var tagVal = tagValue.value;
                        AssetTagBackgroundStatusUpdate(AssetsTableGetValueElement(cellTag), tagVal, tagValue.inAlarm);
                        if (tagVal == null) {
                            continue;
                        }
                        // Put value to cell
                        if (tagValue.dataType == 'Float' ||
                            tagValue.dataType == 'Double') {
                            tagVal = parseFloat(tagVal.toFixed(2));
                        }
                        if (tagValue.rangeMax != null && tagValue.rangeMin != null) {
                            ValueBarForNumberUpdate(AssetsTableGetValueElement(cellTag), tagVal, tagValue.rangeMax, tagValue.rangeMin);
                            continue;
                        }
                        if (tagValue.dataType == 'Boolean') {
                            var bool = tagVal;
                            DotForBoolean(AssetsTableGetValueElement(cellTag), bool);
                            continue;
                        }
                        AssetsTableGetValueElement(cellTag).innerHTML = '';
                        AssetsTableGetValueElement(cellTag).appendChild(document.createTextNode(String(tagVal)));
                    }
                });
            },
        });
    }, 10000);
}
function AssetsTablePrepareCell(cell) {
    if (cell == null) {
        return;
    }
    var div = document.createElement('div');
    div.classList.add('align-middle');
    //cell.classList.add('d-flex');
    cell.appendChild(div);
    //div.style.height = cell.offsetHeight + 'px';
}
function AssetsTableGetValueElement(cell) {
    if (cell == null) {
        return;
    }
    var valueElem = cell.getElementsByTagName('div')[0];
    return valueElem;
}
function AssetTagBackgroundStatusUpdate(elem, value, inAlarm) {
    var noCommClassName = 'bg-site-no-comm';
    var alarmClassName = 'table-alarm-indicator';
    elem.classList.remove(noCommClassName);
    elem.classList.remove(alarmClassName);
    if (value == null) {
        elem.classList.add(noCommClassName);
        return;
    }
    if (inAlarm) {
        elem.classList.add(alarmClassName);
    }
}
function AssetsStatusBarUpdate(okCnt, alarmCnt) {
    var ok = document.getElementById('statusBarAssetsOkCnt');
    var alarm = document.getElementById('statusBarAssetsAlarmCnt');
    ok.innerHTML = String(okCnt);
    alarm.innerHTML = String(alarmCnt);
}
function DotForBoolean(elem, state) {
    if (elem == null) {
        return;
    }
    elem.innerHTML = '';
    var div = document.createElement('div');
    div.classList.add('d-flex');
    div.classList.add('justify-content-center');
    div.classList.add('align-items-center');
    var span = document.createElement('span');
    span.classList.add('dot');
    var spanInner = document.createElement('span');
    spanInner.classList.add('dotTextCenterDiv');
    spanInner.innerHTML = 'F';
    elem.appendChild(div);
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
function ValueBarForNumber(elem, value, rangeMax, rangeMin) {
    if (elem == null) {
        return;
    }
    elem.innerHTML = '';
    if ((rangeMax <= rangeMin) || (value < rangeMin) || (value > rangeMax)) {
        elem.appendChild(document.createTextNode(String(value)));
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
    elem.appendChild(divOuter);
    divOuter.appendChild(divInner1);
    divOuter.appendChild(divInner2);
    divInner1.appendChild(divInner1Inner);
    divInner2.appendChild(divInner2Inner);
    divInner1Inner.appendChild(document.createTextNode(String(value)));
}
function ValueBarForNumberUpdate(elem, value, rangeMax, rangeMin) {
    if (elem == null) {
        return;
    }
    if ((rangeMax <= rangeMin) || (value < rangeMin) || (value > rangeMax)) {
        elem.innerHTML = '';
        elem.appendChild(document.createTextNode(String(value)));
        return;
    }
    var valueBarValue = elem.getElementsByClassName('valuebar-value')[0];
    var valueBarProgress = elem.getElementsByClassName('progress-bar')[0];
    var fillProcent = ((value - rangeMin) / (rangeMax - rangeMin)) * 100;
    valueBarProgress.style.width = fillProcent + '%';
    valueBarValue.innerHTML = String(value);
}
//# sourceMappingURL=monitor.js.map
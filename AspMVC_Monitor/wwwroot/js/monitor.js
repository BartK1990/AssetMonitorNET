import * as TableAsset from "./Monitor/tableAsset.js";
var Monitor;
(function (Monitor) {
    const SharedTagTableId = 'tableAssets';
    var SharedTagInitNumberOfColumns = 0;
    var TagSetId = null;
    var SharedTagSets = null;
    var SharedTagTableRows = null;
    var GetAssetsLiveDataInterval = null;
    document.addEventListener("DOMContentLoaded", function (event) {
        initMonitor();
    });
    function initMonitor() {
        var tableAssets = document.getElementById(SharedTagTableId);
        SharedTagInitNumberOfColumns = tableAssets.tHead.children[0].childElementCount;
        let buttonsWrapper = document.getElementById('buttonsTagSets');
        let buttons = buttonsWrapper.querySelectorAll('button');
        buttons.forEach(function (button) {
            button.addEventListener('click', function () {
                GetSharedTagColumns(Number(button.value));
            });
        });
        GetAssetsLiveData();
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
                    AssetsTableText(AssetsTableGetValueElement(cellName), assetData.name);
                    var cellIp = newRow.insertCell();
                    AssetsTablePrepareCell(cellIp);
                    AssetsTableText(AssetsTableGetValueElement(cellIp), assetData.ipAddress);
                    var cellInAlarm = newRow.insertCell();
                    AssetsTablePrepareCell(cellInAlarm);
                    AssetTagBackgroundStatusUpdate(cellInAlarm, assetData.inAlarm, assetData.inAlarm);
                    AssetsTableDotForBoolean(AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);
                    if (SharedTagSets == null) {
                        return;
                    }
                    for (var i = 0; i < SharedTagSets.length; i++) {
                        var cellTag = newRow.insertCell();
                        AssetsTablePrepareCell(cellTag);
                        var tagValue = null;
                        for (var j = 0; j < assetData.tags.length; j++) {
                            if (assetData.tags[j].sharedTagId == SharedTagSets[i].id) {
                                tagValue = assetData.tags[j];
                                break;
                            }
                        }
                        if (tagValue == null) {
                            continue;
                        }
                        var tagVal = tagValue.value;
                        AssetTagBackgroundStatusUpdate(cellTag, tagVal, tagValue.inAlarm);
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
                            AssetsTableDotForBoolean(AssetsTableGetValueElement(cellTag), bool);
                            continue;
                        }
                        AssetsTableText(AssetsTableGetValueElement(cellTag), tagVal);
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
                        AssetTagBackgroundStatusUpdate(cellInAlarm, assetData.inAlarm, assetData.inAlarm);
                        AssetsTableDotForBoolean(AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);
                        if (SharedTagSets == null) {
                            return;
                        }
                        for (var i = 0; i < SharedTagSets.length; i++) {
                            var cellTag = row.cells[i + SharedTagInitNumberOfColumns];
                            var tagValue = null;
                            for (var j = 0; j < assetData.tags.length; j++) {
                                if (assetData.tags[j].sharedTagId == SharedTagSets[i].id) {
                                    tagValue = assetData.tags[j];
                                    break;
                                }
                            }
                            if (tagValue == null) {
                                continue;
                            }
                            var tagVal = tagValue.value;
                            AssetTagBackgroundStatusUpdate(cellTag, tagVal, tagValue.inAlarm);
                            if (tagVal == null) {
                                continue;
                            }
                            // Put value to cell
                            if (tagValue.dataType == 'Float' ||
                                tagValue.dataType == 'Double') {
                                tagVal = parseFloat(tagVal.toFixed(2));
                            }
                            if (tagValue.rangeMax != null && tagValue.rangeMin != null) {
                                TableAsset.ValueBarForNumberUpdate(AssetsTableGetValueElement(cellTag), tagVal, tagValue.rangeMax, tagValue.rangeMin);
                                continue;
                            }
                            if (tagValue.dataType == 'Boolean') {
                                var bool = tagVal;
                                AssetsTableDotForBoolean(AssetsTableGetValueElement(cellTag), bool);
                                continue;
                            }
                            AssetsTableTextUpdate(AssetsTableGetValueElement(cellTag), tagVal);
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
        var divInner = document.createElement('div');
        div.classList.add('asset-table-cell-div-alarm');
        divInner.classList.add('asset-table-cell-div-inner');
        cell.appendChild(div);
        div.appendChild(divInner);
    }
    Monitor.AssetsTablePrepareCell = AssetsTablePrepareCell;
    function AssetsTableGetValueElement(cell) {
        if (cell == null) {
            return;
        }
        var valueElem = cell.getElementsByTagName('div')[0].getElementsByTagName('div')[0];
        return valueElem;
    }
    Monitor.AssetsTableGetValueElement = AssetsTableGetValueElement;
    function AssetTagBackgroundStatusUpdate(cell, value, inAlarm) {
        var alarmElem = cell.getElementsByClassName('asset-table-cell-div-alarm')[0];
        var noCommClassName = 'bg-site-no-comm';
        var alarmClassName = 'table-alarm-indicator';
        alarmElem.classList.remove(noCommClassName);
        alarmElem.classList.remove(alarmClassName);
        if (value == null) {
            alarmElem.classList.add(noCommClassName);
            return;
        }
        if (inAlarm) {
            alarmElem.classList.add(alarmClassName);
        }
    }
    Monitor.AssetTagBackgroundStatusUpdate = AssetTagBackgroundStatusUpdate;
    function AssetsStatusBarUpdate(okCnt, alarmCnt) {
        var ok = document.getElementById('statusBarAssetsOkCnt');
        var alarm = document.getElementById('statusBarAssetsAlarmCnt');
        ok.innerHTML = String(okCnt);
        alarm.innerHTML = String(alarmCnt);
    }
    Monitor.AssetsStatusBarUpdate = AssetsStatusBarUpdate;
    function AssetsTableText(elem, value) {
        if (elem == null) {
            return;
        }
        var div = document.createElement('div');
        elem.appendChild(div);
        div.innerHTML = String(value);
    }
    Monitor.AssetsTableText = AssetsTableText;
    function AssetsTableTextUpdate(elem, value) {
        if (elem == null) {
            return;
        }
        var childDiv = elem.getElementsByTagName('div')[0];
        childDiv.innerHTML = String(value);
    }
    Monitor.AssetsTableTextUpdate = AssetsTableTextUpdate;
    function AssetsTableDotForBoolean(elem, state) {
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
    Monitor.AssetsTableDotForBoolean = AssetsTableDotForBoolean;
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
    Monitor.ValueBarForNumber = ValueBarForNumber;
})(Monitor || (Monitor = {}));
//# sourceMappingURL=monitor.js.map
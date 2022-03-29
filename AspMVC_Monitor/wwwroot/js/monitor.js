import { TableAsset } from "./Monitor/tableAsset.js";
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
                TableAsset.AssetsStatusBarUpdate(assets.okCnt, assets.inAlarmCnt);
                var tableAssets = document.getElementById(SharedTagTableId);
                var tableAssetsBody = tableAssets.tBodies[0];
                tableAssetsBody.innerHTML = '';
                SharedTagTableRows = new Map();
                $.each(assets.assets, function (i, assetData) {
                    var newRow = tableAssetsBody.insertRow();
                    SharedTagTableRows.set(assetData.id, newRow);
                    var cellName = TableAsset.AssetsTablePrepareCell(newRow);
                    TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellName), assetData.name);
                    var cellIp = TableAsset.AssetsTablePrepareCell(newRow);
                    TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellIp), assetData.ipAddress);
                    var cellInAlarm = TableAsset.AssetsTablePrepareCell(newRow);
                    TableAsset.AssetTagBackgroundStatusUpdate(cellInAlarm, assetData.inAlarm, assetData.inAlarm);
                    TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);
                    if (SharedTagSets == null) {
                        return;
                    }
                    for (var i = 0; i < SharedTagSets.length; i++) {
                        var cellTag = TableAsset.AssetsTablePrepareCell(newRow);
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
                        TableAsset.AssetTagBackgroundStatusUpdate(cellTag, tagVal, tagValue.inAlarm);
                        if (tagVal == null) {
                            tagVal = 0;
                        }
                        // Put value to cell
                        if (tagValue.dataType == 'Float' ||
                            tagValue.dataType == 'Double') {
                            tagVal = parseFloat(tagVal.toFixed(2));
                        }
                        if (tagValue.rangeMax != null && tagValue.rangeMin != null) {
                            TableAsset.ValueBarForNumber(TableAsset.AssetsTableGetValueElement(cellTag), tagVal, tagValue.rangeMax, tagValue.rangeMin);
                            continue;
                        }
                        if (tagValue.dataType == 'Boolean') {
                            var bool = tagVal;
                            TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellTag), bool);
                            continue;
                        }
                        TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellTag), tagVal);
                    }
                });
                GetAssetsLiveDataUpdate();
            },
        });
    }
    function GetAssetsLiveDataUpdate() {
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
        GetAssetsLiveDataInterval = setInterval(function () {
            $.ajax({
                type: 'post',
                url: url,
                dataType: 'json',
                success: function (data) {
                    var assets = data;
                    TableAsset.AssetsStatusBarUpdate(assets.okCnt, assets.inAlarmCnt);
                    $.each(assets.assets, function (i, assetData) {
                        var row = SharedTagTableRows.get(assetData.id);
                        var cellInAlarm = row.cells[2];
                        TableAsset.AssetTagBackgroundStatusUpdate(cellInAlarm, assetData.inAlarm, assetData.inAlarm);
                        TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);
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
                            TableAsset.AssetTagBackgroundStatusUpdate(cellTag, tagVal, tagValue.inAlarm);
                            if (tagVal == null) {
                                continue;
                            }
                            // Put value to cell
                            if (tagValue.dataType == 'Float' ||
                                tagValue.dataType == 'Double') {
                                tagVal = parseFloat(tagVal.toFixed(2));
                            }
                            if (tagValue.rangeMax != null && tagValue.rangeMin != null) {
                                TableAsset.ValueBarForNumberUpdate(TableAsset.AssetsTableGetValueElement(cellTag), tagVal, tagValue.rangeMax, tagValue.rangeMin);
                                continue;
                            }
                            if (tagValue.dataType == 'Boolean') {
                                var bool = tagVal;
                                TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellTag), bool);
                                continue;
                            }
                            TableAsset.AssetsTableTextUpdate(TableAsset.AssetsTableGetValueElement(cellTag), tagVal);
                        }
                    });
                },
            });
        }, 10000);
    }
})(Monitor || (Monitor = {}));
//# sourceMappingURL=monitor.js.map
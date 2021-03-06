import { TableAsset } from "./Monitor/tableAsset.js";
import { Site } from "./site.js";
var Monitor;
(function (Monitor) {
    var RazorUrlGetAssetsLiveData;
    var RazorUrlGetAssetsLiveData_tagSetId;
    var RazorUrlHomeGetSharedTagColumns_tagSetId;
    var RazorUrlDetailsIndex_assetId;
    const ScanTimeNewAssetsValues = 10000;
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
        // Razor Urls
        RazorUrlGetAssetsLiveData = document.getElementById('RazorUrlGetAssetsLiveData').getAttribute('data');
        RazorUrlGetAssetsLiveData_tagSetId = document.getElementById('RazorUrlGetAssetsLiveData_tagSetId').getAttribute('data');
        RazorUrlHomeGetSharedTagColumns_tagSetId = document.getElementById('RazorUrlHomeGetSharedTagColumns_tagSetId').getAttribute('data');
        RazorUrlDetailsIndex_assetId = document.getElementById('RazorUrlDetailsIndex_assetId').getAttribute('data');
        // Init part
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
            url: Site.UrlActionWithParameter(RazorUrlHomeGetSharedTagColumns_tagSetId, tagSetId),
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
            url = Site.UrlActionWithParameter(RazorUrlGetAssetsLiveData_tagSetId, TagSetId);
        }
        else {
            url = RazorUrlGetAssetsLiveData;
        }
        // Build table
        $.ajax({
            type: 'post',
            url: url,
            dataType: 'json',
            success: function (data) {
                var assets = data;
                if (assets.assets.length <= 0) {
                    return;
                }
                TableAsset.AssetsStatusBarUpdate(assets.okCnt, assets.inAlarmCnt);
                var tableAssets = document.getElementById(SharedTagTableId);
                var tableAssetsBody = tableAssets.tBodies[0];
                tableAssetsBody.innerHTML = '';
                SharedTagTableRows = new Map();
                $.each(assets.assets, function (i, assetData) {
                    var newRow = tableAssetsBody.insertRow();
                    SharedTagTableRows.set(assetData.id, newRow);
                    var cellName = TableAsset.AssetsTablePrepareCell(newRow);
                    // Button to redirect
                    var elemName = TableAsset.AssetsTableGetValueElement(cellName);
                    var buttonAssetName = document.createElement('button');
                    buttonAssetName.classList.add('btn');
                    buttonAssetName.classList.add('btn-secondary');
                    buttonAssetName.value = String(assetData.id);
                    buttonAssetName.innerHTML = assetData.name;
                    elemName.appendChild(buttonAssetName);
                    buttonAssetName.addEventListener('click', function () {
                        window.location.href = Site.UrlActionWithParameter(RazorUrlDetailsIndex_assetId, assetData.id);
                    });
                    var cellIp = TableAsset.AssetsTablePrepareCell(newRow);
                    TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellIp), assetData.ipAddress);
                    var cellInAlarm = TableAsset.AssetsTablePrepareCell(newRow);
                    TableAsset.AssetTagBackgroundStatusUpdate(cellInAlarm, assetData.inAlarm, assetData.inAlarm);
                    TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);
                    UpdateTagsInRow(SharedTagSets, assetData, newRow, false);
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
            url = Site.UrlActionWithParameter(RazorUrlGetAssetsLiveData_tagSetId, TagSetId);
        }
        else {
            url = RazorUrlGetAssetsLiveData;
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
                        UpdateTagsInRow(SharedTagSets, assetData, row, true);
                    });
                },
            });
        }, ScanTimeNewAssetsValues);
    }
    function UpdateTagsInRow(sharedTagSets, assetData, row, updateFlag) {
        if (sharedTagSets == null) {
            return;
        }
        for (var i = 0; i < sharedTagSets.length; i++) {
            var cellTag;
            if (updateFlag) {
                cellTag = row.cells[i + SharedTagInitNumberOfColumns];
            }
            else {
                cellTag = TableAsset.AssetsTablePrepareCell(row);
            }
            var tagValue = null;
            for (var j = 0; j < assetData.tags.length; j++) {
                if (assetData.tags[j].sharedTagId == SharedTagSets[i].id) {
                    tagValue = assetData.tags[j];
                    break;
                }
            }
            UpdateTagValue(cellTag, tagValue, updateFlag);
        }
    }
    function UpdateTagValue(cellTag, tagValue, updateFlag) {
        if (tagValue == null) {
            return;
        }
        var tagVal = tagValue.value;
        TableAsset.AssetTagBackgroundStatusUpdate(cellTag, tagVal, tagValue.inAlarm);
        if (tagVal == null) {
            if (updateFlag) {
                return;
            }
            else {
                tagVal = 0;
            }
        }
        if (tagValue.dataType == 'Float' ||
            tagValue.dataType == 'Double') {
            tagVal = parseFloat(tagVal.toFixed(2));
        }
        if (tagValue.rangeMax != null && tagValue.rangeMin != null) {
            if (updateFlag) {
                TableAsset.ValueBarForNumberUpdate(TableAsset.AssetsTableGetValueElement(cellTag), tagVal, tagValue.rangeMax, tagValue.rangeMin);
            }
            else {
                TableAsset.ValueBarForNumber(TableAsset.AssetsTableGetValueElement(cellTag), tagVal, tagValue.rangeMax, tagValue.rangeMin);
            }
            return;
        }
        if (tagValue.dataType == 'Boolean') {
            var bool = tagVal;
            TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellTag), bool);
            return;
        }
        if (updateFlag) {
            TableAsset.AssetsTableTextUpdate(TableAsset.AssetsTableGetValueElement(cellTag), tagVal);
        }
        else {
            TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellTag), tagVal);
        }
    }
})(Monitor || (Monitor = {}));
//# sourceMappingURL=monitor.js.map
import { SharedTagSet } from "./Monitor/sharedTagSet";
import { Tag } from "./Monitor/tag";
import { Assets } from "./Monitor/assets";
import { TableAsset } from "./Monitor/tableAsset.js";
import { AssetData } from "./Monitor/assetData";

module Monitor {

    const ScanTimeNewAssetsValues: number = 10000;
    const SharedTagTableId: string = 'tableAssets';
    var SharedTagInitNumberOfColumns: number = 0;
    var TagSetId: number = null;
    var SharedTagSets: SharedTagSet[] = null;
    var SharedTagTableRows: Map<number, HTMLTableRowElement> = null;
    var GetAssetsLiveDataInterval: number = null;

    document.addEventListener("DOMContentLoaded", function (event) {
        initMonitor();
    });

    function initMonitor() {
        var tableAssets: HTMLTableElement = <HTMLTableElement>document.getElementById(SharedTagTableId);
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
                        if (i >= SharedTagInitNumberOfColumns) {
                            rows[j].deleteCell(SharedTagInitNumberOfColumns);
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
        var url: string;
        if (TagSetId != null) {
            url = 'GetAssetsLiveData?tagSetId=' + TagSetId;

        } else {
            url = 'GetAssetsLiveData';
        }
        // Build table
        $.ajax({
            type: 'post',
            url: url,
            dataType: 'json',
            success: function (data) {
                var assets: Assets = data;

                if (assets.assets.length <= 0) {
                    return;
                }

                TableAsset.AssetsStatusBarUpdate(assets.okCnt, assets.inAlarmCnt);

                var tableAssets: HTMLTableElement = <HTMLTableElement>document.getElementById(SharedTagTableId);
                var tableAssetsBody = tableAssets.tBodies[0];

                tableAssetsBody.innerHTML = '';
                SharedTagTableRows = new Map<number, HTMLTableRowElement>();
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
                        window.location.href = '/Details/Index?assetId=' + assetData.id;
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

        var url: string;
        if (TagSetId != null) {
            url = 'GetAssetsLiveData?tagSetId=' + TagSetId;

        } else {
            url = 'GetAssetsLiveData';
        }

        GetAssetsLiveDataInterval = setInterval(function () {
            $.ajax({
                type: 'post',
                url: url,
                dataType: 'json',
                success: function (data) {
                    var assets: Assets = data;
                    TableAsset.AssetsStatusBarUpdate(assets.okCnt, assets.inAlarmCnt);
                    $.each(assets.assets, function (i, assetData) {
                        var row: HTMLTableRowElement = SharedTagTableRows.get(assetData.id);
                        var cellInAlarm = row.cells[2];
                        TableAsset.AssetTagBackgroundStatusUpdate(cellInAlarm, assetData.inAlarm, assetData.inAlarm);
                        TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);

                        UpdateTagsInRow(SharedTagSets, assetData, row, true);

                    });
                },
            });
        }, ScanTimeNewAssetsValues);
    }

    function UpdateTagsInRow(sharedTagSets: SharedTagSet[], assetData: AssetData, row: HTMLTableRowElement, updateFlag: boolean) {
        if (sharedTagSets == null) {
            return;
        }

        for (var i = 0; i < sharedTagSets.length; i++) {
            var cellTag: HTMLTableCellElement;
            if (updateFlag) {
                cellTag = row.cells[i + SharedTagInitNumberOfColumns];
            } else {
                cellTag = TableAsset.AssetsTablePrepareCell(row);
            }

            var tagValue: Tag = null;
            for (var j = 0; j < assetData.tags.length; j++) {
                if (assetData.tags[j].sharedTagId == SharedTagSets[i].id) {
                    tagValue = assetData.tags[j];
                    break;
                }
            }

            UpdateTagValue(cellTag, tagValue, updateFlag);
        }
    }

    function UpdateTagValue(cellTag: HTMLTableCellElement, tagValue: Tag, updateFlag: boolean){
        if (tagValue == null) {
            return;
        }
        var tagVal = tagValue.value;

        TableAsset.AssetTagBackgroundStatusUpdate(cellTag, tagVal, tagValue.inAlarm);

        if (tagVal == null) {
            if (updateFlag) {
                return;
            } else {
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
            } else {
                TableAsset.ValueBarForNumber(TableAsset.AssetsTableGetValueElement(cellTag), tagVal, tagValue.rangeMax, tagValue.rangeMin);
            }
            return;
        }
        if (tagValue.dataType == 'Boolean') {
            var bool = <boolean>tagVal;
            TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellTag), bool);
            return;
        }
        if (updateFlag) {
            TableAsset.AssetsTableTextUpdate(TableAsset.AssetsTableGetValueElement(cellTag), tagVal);
        } else {
            TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellTag), tagVal);
        }
    }

}

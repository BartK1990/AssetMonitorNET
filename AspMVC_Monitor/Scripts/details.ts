import { AssetData } from "./Monitor/assetData";
import { TableAsset } from "./Monitor/tableAsset.js";

module Details {

    const ScanTimeNewAssetValues: number = 10000;
    const TagTableHtmlId = 'tableAsset';
    const AssetTableInfoHtmlId = 'tableAssetInfo';
    const AssetSelectHtmlId = 'selectAsset';
    const AssetSelectSubmitHtmlId = 'submitSelectAsset';
    const ButtonGetSnmpDataHtmlId = 'buttonGetSnmpData';

    var TagTableRows: Map<number, HTMLTableRowElement> = null;
    var GetAssetLiveDataInterval: number = null;
    var AssetId: number = null;

    document.addEventListener("DOMContentLoaded", function (event) {
        initDetails();
    });

    function initDetails() {
        SetAssetId();

        var assetSelectSubmit = document.getElementById(AssetSelectSubmitHtmlId);
        assetSelectSubmit.addEventListener('click', function () {
            SetAssetId();
        });

        var buttonGetSnmpData = document.getElementById(ButtonGetSnmpDataHtmlId);
        buttonGetSnmpData.addEventListener('click', function () {
            UpdateSnmpDataOrder();
        });
    }

    function SetAssetId() {
        var assetSelect: HTMLSelectElement = <HTMLSelectElement>document.getElementById(AssetSelectHtmlId);
        var selectedId = assetSelect.options[assetSelect.selectedIndex].value;

        var selectedIdParsed = Number.parseInt(selectedId);
        if (Number.isNaN(selectedIdParsed)) {
            return;
        }
        AssetId = selectedIdParsed;
        GetAssetLiveData();
    }

    function UpdateSnmpDataOrder() {
        if (AssetId == null) {
            return;
        }

        var url: string = '/Details/UpdateAssetSnmpData?assetId=' + AssetId;
        $.post(url);
    }

    function GetAssetLiveData() {
        if (AssetId == null) {
            return;
        }

        var url: string = '/Details/GetAssetLiveData?assetId=' + AssetId;

        // Build table
        $.ajax({
            type: 'post',
            url: url,
            dataType: 'json',
            success: function (data) { 
                var assetData: AssetData = data;

                var tableAsset: HTMLTableElement = <HTMLTableElement>document.getElementById(TagTableHtmlId);
                var tableAssetInfo: HTMLTableElement = <HTMLTableElement>document.getElementById(AssetTableInfoHtmlId);
                var tableAssetBody = tableAsset.tBodies[0];
                var tableAssetInfoBody = tableAssetInfo.tBodies[0];
                tableAssetBody.innerHTML = '';
                tableAssetInfoBody.innerHTML = '';

                var newRow = tableAssetInfoBody.insertRow();

                var cellName = TableAsset.AssetsTablePrepareCell(newRow);
                TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellName), assetData.name);

                var cellIp = TableAsset.AssetsTablePrepareCell(newRow);
                TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellIp), assetData.ipAddress);

                var cellInAlarm = TableAsset.AssetsTablePrepareCell(newRow);
                TableAsset.AssetTagBackgroundStatusUpdate(cellInAlarm, assetData.inAlarm, assetData.inAlarm);
                TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);

                TagTableRows = new Map<number, HTMLTableRowElement>();
                UpdateTagsInTable(assetData, tableAssetBody, tableAssetInfoBody, false);

                GetAssetLiveDataUpdate(tableAssetBody ,tableAssetInfoBody);
            },
        });
    }

    function GetAssetLiveDataUpdate(tableAssetBody: HTMLTableSectionElement,
        tableAssetInfoBody: HTMLTableSectionElement) {
        if (GetAssetLiveDataInterval != null) {
            clearInterval(GetAssetLiveDataInterval);
        }

        if (AssetId == null) {
            return;
        }

        var url: string = '/Details/GetAssetLiveData?assetId=' + AssetId;

        GetAssetLiveDataInterval = setInterval(function () {
            $.ajax({
                type: 'post',
                url: url,
                dataType: 'json',
                success: function (data) {
                    var assetData: AssetData = data;

                    var row = tableAssetInfoBody.rows[0];

                    var cellInAlarm = row.cells[2];
                    TableAsset.AssetTagBackgroundStatusUpdate(cellInAlarm, assetData.inAlarm, assetData.inAlarm);
                    TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);

                    UpdateTagsInTable(assetData, tableAssetBody, tableAssetInfoBody, true);
                },
            });
        }, ScanTimeNewAssetValues);
    }

    function UpdateTagsInTable(assetData: AssetData, tableAssetBody: HTMLTableSectionElement,
        tableAssetInfoBody: HTMLTableSectionElement, updateFlag: boolean) {
        assetData.tags.forEach(function (tag) {
            var row: HTMLTableRowElement;
            if (updateFlag) {
                row = TagTableRows.get(tag.id);
            } else {
                row = tableAssetBody.insertRow();
                TagTableRows.set(tag.id, row);
            }

            var cellTagname: HTMLTableCellElement;
            var cellTagValue: HTMLTableCellElement;
            if (updateFlag) {
                cellTagname = row.cells[0];
                cellTagValue = row.cells[1];
            } else {
                cellTagname = TableAsset.AssetsTablePrepareCell(row);
                cellTagValue = TableAsset.AssetsTablePrepareCell(row);
            }

            if (!updateFlag) {
                TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellTagname), tag.tagname);
            }
            TableAsset.UpdateTagValue(cellTagValue, tag, updateFlag);
        });
    }

}



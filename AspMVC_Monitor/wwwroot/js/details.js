import { TableAsset } from "./Monitor/tableAsset.js";
var Details;
(function (Details) {
    const ScanTimeNewAssetValues = 10000;
    const TagTableHtmlId = 'tableAsset';
    const AssetTableInfoHtmlId = 'tableAssetInfo';
    const AssetSelectHtmlId = 'selectAsset';
    const AssetSelectSubmitHtmlId = 'submitSelectAsset';
    const ButtonGetSnmpDataHtmlId = 'buttonGetSnmpData';
    var TagTableRows = null;
    var GetAssetLiveDataInterval = null;
    var AssetId = null;
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
        var assetSelect = document.getElementById(AssetSelectHtmlId);
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
        var url = '/Details/UpdateAssetSnmpData?assetId=' + AssetId;
        $.post(url);
    }
    function GetAssetLiveData() {
        if (AssetId == null) {
            return;
        }
        var url = '/Details/GetAssetLiveData?assetId=' + AssetId;
        // Build table
        $.ajax({
            type: 'post',
            url: url,
            dataType: 'json',
            success: function (data) {
                var assetData = data;
                var tableAsset = document.getElementById(TagTableHtmlId);
                var tableAssetInfo = document.getElementById(AssetTableInfoHtmlId);
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
                TagTableRows = new Map();
                UpdateTagsInTable(assetData, tableAssetBody, tableAssetInfoBody, false);
                GetAssetLiveDataUpdate(tableAssetBody, tableAssetInfoBody);
            },
        });
    }
    function GetAssetLiveDataUpdate(tableAssetBody, tableAssetInfoBody) {
        if (GetAssetLiveDataInterval != null) {
            clearInterval(GetAssetLiveDataInterval);
        }
        if (AssetId == null) {
            return;
        }
        var url = '/Details/GetAssetLiveData?assetId=' + AssetId;
        GetAssetLiveDataInterval = setInterval(function () {
            $.ajax({
                type: 'post',
                url: url,
                dataType: 'json',
                success: function (data) {
                    var assetData = data;
                    var row = tableAssetInfoBody.rows[0];
                    var cellInAlarm = row.cells[2];
                    TableAsset.AssetTagBackgroundStatusUpdate(cellInAlarm, assetData.inAlarm, assetData.inAlarm);
                    TableAsset.AssetsTableDotForBoolean(TableAsset.AssetsTableGetValueElement(cellInAlarm), assetData.inAlarm);
                    UpdateTagsInTable(assetData, tableAssetBody, tableAssetInfoBody, true);
                },
            });
        }, ScanTimeNewAssetValues);
    }
    function UpdateTagsInTable(assetData, tableAssetBody, tableAssetInfoBody, updateFlag) {
        assetData.tags.forEach(function (tag) {
            var row;
            if (updateFlag) {
                row = TagTableRows.get(tag.id);
            }
            else {
                row = tableAssetBody.insertRow();
                TagTableRows.set(tag.id, row);
            }
            var cellTagname;
            var cellTagValue;
            if (updateFlag) {
                cellTagname = row.cells[0];
                cellTagValue = row.cells[1];
            }
            else {
                cellTagname = TableAsset.AssetsTablePrepareCell(row);
                cellTagValue = TableAsset.AssetsTablePrepareCell(row);
            }
            if (!updateFlag) {
                TableAsset.AssetsTableText(TableAsset.AssetsTableGetValueElement(cellTagname), tag.tagname);
            }
            TableAsset.UpdateTagValue(cellTagValue, tag, updateFlag);
        });
    }
})(Details || (Details = {}));
//# sourceMappingURL=details.js.map
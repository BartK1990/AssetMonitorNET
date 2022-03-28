var Details;
(function (Details) {
    const TagTableId = 'tableAsset';
    const AssetNameParagraphId = 'paragraphAsset';
    document.addEventListener("DOMContentLoaded", function (event) {
        initDetails();
    });
    function initDetails() {
    }
    function AssetTablePrepareCell(cell) {
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
    function AssetTableGetValueElement(cell) {
        if (cell == null) {
            return;
        }
        var valueElem = cell.getElementsByTagName('div')[0].getElementsByTagName('div')[0];
        return valueElem;
    }
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
})(Details || (Details = {}));
//# sourceMappingURL=details.js.map
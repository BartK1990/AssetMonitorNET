export var TableAsset;
(function (TableAsset) {
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
    TableAsset.ValueBarForNumberUpdate = ValueBarForNumberUpdate;
    function AssetsTablePrepareCell(row) {
        if (row == null) {
            return;
        }
        var cell = row.insertCell();
        var div = document.createElement('div');
        var divInner = document.createElement('div');
        div.classList.add('asset-table-cell-div-alarm');
        divInner.classList.add('asset-table-cell-div-inner');
        cell.appendChild(div);
        div.appendChild(divInner);
        return cell;
    }
    TableAsset.AssetsTablePrepareCell = AssetsTablePrepareCell;
    function AssetsTableGetValueElement(cell) {
        if (cell == null) {
            return;
        }
        var valueElem = cell.getElementsByTagName('div')[0].getElementsByTagName('div')[0];
        return valueElem;
    }
    TableAsset.AssetsTableGetValueElement = AssetsTableGetValueElement;
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
    TableAsset.AssetTagBackgroundStatusUpdate = AssetTagBackgroundStatusUpdate;
    function AssetsStatusBarUpdate(okCnt, alarmCnt) {
        var ok = document.getElementById('statusBarAssetsOkCnt');
        var alarm = document.getElementById('statusBarAssetsAlarmCnt');
        ok.innerHTML = String(okCnt);
        alarm.innerHTML = String(alarmCnt);
    }
    TableAsset.AssetsStatusBarUpdate = AssetsStatusBarUpdate;
    function AssetsTableText(elem, value) {
        if (elem == null) {
            return;
        }
        var div = document.createElement('div');
        elem.appendChild(div);
        div.innerHTML = String(value);
    }
    TableAsset.AssetsTableText = AssetsTableText;
    function AssetsTableTextUpdate(elem, value) {
        if (elem == null) {
            return;
        }
        var childDiv = elem.getElementsByTagName('div')[0];
        childDiv.innerHTML = String(value);
    }
    TableAsset.AssetsTableTextUpdate = AssetsTableTextUpdate;
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
    TableAsset.AssetsTableDotForBoolean = AssetsTableDotForBoolean;
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
    TableAsset.ValueBarForNumber = ValueBarForNumber;
})(TableAsset || (TableAsset = {}));
//# sourceMappingURL=tableAsset.js.map
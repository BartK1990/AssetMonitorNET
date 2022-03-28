export function ValueBarForNumberUpdate(elem, value, rangeMax, rangeMin) {
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
//# sourceMappingURL=tableAsset.js.map
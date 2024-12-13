
function DateJs(DataDaConvertire) {
    var dataJson = DataDaConvertire;

    let timeSpan = parseInt(dataJson.match(/\d+/));

    let dataToReturn = new Date(timeSpan);

    let DateAndTime = dataToReturn.toLocaleDateString() + ", " + dataToReturn.toLocaleTimeString();
    return DateAndTime;
}

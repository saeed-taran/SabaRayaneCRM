var neshanMapInstance;
export function initNeshanMap(apiKey, lat, lon) {

    if (!lat)
        lat = 35.6892;
    if (!lon)
        lon = 51.389;

    neshanMapInstance = new ol.Map({
        maptype: 'neshan',
        target: 'map',
        key: apiKey,
        poi: true,
        traffic: false,
        view: new ol.View({
            center: ol.proj.fromLonLat([lon, lat]),
            zoom: 14
        })
    });
}

export function SetMapCenter(lat, lon, zoom) {
    if (zoom == undefined)
        zoom = 14;
    neshanMapInstance.getView().animate({ zoom: zoom, center: ol.proj.fromLonLat([lat, lon]) });
}

export function GetCenterLatLon() {
    var centerLatLong = ol.proj.toLonLat(neshanMapInstance.getView().getCenter());
    return centerLatLong.toString();
}
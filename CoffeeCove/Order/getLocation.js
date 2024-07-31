function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(handleLocation, handleError);
        
    }
    else {
        alert("Geolocation is not supported by this browser.");
    }
} 

function handleLocation(position) {

    var lat = position.coords.latitude;
   

    var lon = position.coords.longitude;

    document.getElementById("coord").innerHTML = lat + "," +  lon;

}

function handleError(error) {
    switch (error.code) {
        case error.TIMEOUT:
            alert('Timeout');
            break;
        case error.POSITION_UNAVAILABLE:
            alert('Position unavailable');
            break;
        case error.PERMISSION_DENIED:
            alert('Permission denied');
            break;
        case error.UNKNOWN_ERROR:
            alert('Unknown error');
            break;
    }
}


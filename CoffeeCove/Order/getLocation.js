function getLocation() {
    document.getElementById("overlay").style.display = "block";
    document.getElementById("popupDialog").style.display = "block";

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(handleLocation, handleError);
        
    }
    else {
        alert("Geolocation is not supported by this browser.");
    }
} 

function handleLocation(position) {

    var latMap = position.coords.latitude;
   

    var lonMap = position.coords.longitude;
    

    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: latMap, lng: lonMap },
        zoom: 15
    });

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

function closeMap(event) {
    document.getElementById("overlay").style.display = "none";
    document.getElementById("popupDialog").style.display = "none";
    event.preventDefault();
}

function closeStoreList() {
    document.getElementById("overlay2").style.display = "none";
    document.getElementById("popupDialog2").style.display = "none";
}

function openStoreList() {
    document.getElementById("overlay2").style.display = "block";
    document.getElementById("popupDialog2").style.display = "block";
}

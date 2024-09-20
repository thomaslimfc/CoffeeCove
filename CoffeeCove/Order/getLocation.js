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

    var userLocation = { lat: latMap, lng: lonMap };
    var geocoder = new google.maps.Geocoder();

    var map = new google.maps.Map(document.getElementById('map'), {
        center: userLocation,
        zoom: 15
    });


    var marker = new google.maps.Marker({
        position: userLocation,
        map: map,
        title: 'Your Location!'
    });

    geocoder.geocode({ 'location': latlng }, function (results, status) {
        if (status === 'OK') {
            if (results[0]) {
                // Get the formatted address
                var address = results[0].formatted_address;

                // Update the ASP.NET Label with the address
                document.getElementById("txtAddress1").textContent = address;
            }
            else {
                alert("No results found");
            }
        } else {
            alert("Geocoder failed due to: " + status);
        }
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

function closeMap() {
    document.getElementById("overlay").style.display = "none";
    document.getElementById("popupDialog").style.display = "none";
    //event.preventDefault();
}

function closeStoreList() {
    document.getElementById("overlay2").style.display = "none";
    document.getElementById("popupDialog2").style.display = "none";
}

function openStoreList() {
    document.getElementById("overlay2").style.display = "block";
    document.getElementById("popupDialog2").style.display = "block";
}

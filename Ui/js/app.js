const switchLang = document.getElementById("lang"),
    switchMode = document.getElementById("mode"),
    speedFan = document.getElementById("speedFan"),
    rangeSpeed = document.getElementById("range_value"),
    data = document.querySelector(".diagram.progress"),
    temperCe = document.getElementById("temperature_ce"),
    temperFe = document.getElementById("temperature_fe"),
    barValueHg = document.getElementById("hg"),
    barValuePa = document.getElementById("pa"),
    hygr = document.getElementById("range_value1"),
    animFans = document.getElementById("fan"),
    languages = {
    "fan speed": {
        "ru" : "Скорость",
        "en" : "Fan speed"
    },
    "temperature": {
        "ru": "Температура",
        "en": "Temperature"
    },
    "bar": {
        "ru": "Барометр",
        "en": "Barometr"
    },
    "hygr": {
        "ru": "Гигрометр",
        "en": "Hygrometr"
    }
};
function animate(){
    if (rangeSpeed.value >= 1 && rangeSpeed.value < 30)
    {
        animFans.style.animationDuration = "5s";
    }
    else if (rangeSpeed.value >= 30 && rangeSpeed.value < 60 ){
        animFans.style.animationDuration = "3s";
    }
    else if (rangeSpeed.value >= 60 && rangeSpeed.value < 80){
        animFans.style.animationDuration = "2s"
    }
    else if (rangeSpeed.value >= 80 && rangeSpeed.value < 100){
        animFans.style.animationDuration = "1s"
    }
    else if (rangeSpeed.value == 0){
        animFans.style.animationDuration = "0s"
    }
    
}

switchMode.addEventListener ("input", function() {
    let theme = document.getElementById("theme")

    if (theme.getAttribute("href") == "/css/day_mode.css"){
        theme.href = "/css/night_mode.css" 
    }
    else {
        theme.href = "/css/day_mode.css"
    }
});

switchLang.addEventListener ("input", function() {       
    if (document.getElementById("lang_fan").innerHTML == languages["fan speed"].en)
    {
        document.getElementById("lang_fan").innerHTML = languages["fan speed"].ru
        document.getElementById("lang_temp").innerHTML = languages.temperature.ru
        document.getElementById("lang_bar").innerHTML = languages.bar.ru
        document.getElementById("lang_hygr").innerHTML = languages.hygr.ru
    }
    else if (document.getElementById("lang_fan").innerHTML == languages["fan speed"].ru){
        document.getElementById("lang_fan").innerHTML = languages["fan speed"].en
        document.getElementById("lang_temp").innerHTML = languages.temperature.en
        document.getElementById("lang_bar").innerHTML = languages.bar.en
        document.getElementById("lang_hygr").innerHTML = languages.hygr.en
    }
});


speedFan.addEventListener("input", function() {
    rangeSpeed.value = this.value;
    rangeSpeed.innerHTML = rangeSpeed.value;
    data.setAttribute('data-percent', rangeSpeed.value)
    animate()
  });


const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("/fan")
            .build();

        hubConnection.on("Receiver", function (myObj){
            temperCe.innerHTML = myObj.tarmValueC.ToFixed(1);
            temperFe.innerHTML = myObj.tarmValueF.ToFixed(1);
            barValueHg.innerHTML = myObj.barValueMGH;
            barValuePa.innerHTML = myObj.barValuePascal;
            hygr.innerHTML = myObj.gigValue;
        });

        hubConnection.on("StartSpeed", function (value) {
            rangeSpeed.value = value;
            rangeSpeed.innerHTML = rangeSpeed.value;
            data.setAttribute('data-percent', rangeSpeed.value);
            speedFan.value = value;
        });

        speedFan.addEventListener("input", function () {
            rangeSpeed.value = this.value;
            hubConnection.invoke("ReceiveData", rangeSpeed.value);
        });
        hubConnection.start();





 

    
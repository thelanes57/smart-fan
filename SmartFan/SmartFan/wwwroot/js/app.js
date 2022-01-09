const switchLang = document.getElementById("lang"),
    switchMode = document.getElementById("mode"),
    speed = document.getElementById("speedFan"),
    rangeSpeed = document.getElementById("range_value"),
    data = document.querySelector(".diagram.progress"),
    dataHygrValue = document.querySelector(".diagram1.progress"),
    tempValueC = document.getElementById("temperature_ce"),
    tempValueF = document.getElementById("temperature_fe"),
    barValueMHg = document.getElementById("hg"),
    barValuePascal = document.getElementById("pa"),
    hygrValue = document.getElementById("range_value1"),
    animFanB = document.getElementById("fan"),
    animFanW = document.getElementById("fanWhite"),
    turnOff = document.getElementById("turnOff"),
    languages =
{
    "fan speed": 
    {
        "ru" : "Скорость",
        "en" : "Fan speed"
    },
    "temperature": 
    {
        "ru": "Температура",
        "en": "Temperature"
    },
    "bar": 
    {
        "ru": "Барометр",
        "en": "Barometr"
    },
    "hygr": 
    {
        "ru": "Гигрометр",
        "en": "Hygrometr"
    }
};

function animateFans()
{
    if (rangeSpeed.value >= 1 && rangeSpeed.value < 30)
    {
        animFanB.style.animationDuration = "5s"
        animFanW.style.animationDuration = "5s"
    }
    else if (rangeSpeed.value >= 30 && rangeSpeed.value < 60)
    {
        animFanB.style.animationDuration = "3s"
        animFanW.style.animationDuration = "3s"
    }
    else if (rangeSpeed.value >= 60 && rangeSpeed.value < 80)
    {
        animFanB.style.animationDuration = "2s"
        animFanW.style.animationDuration = "2s"
    }
    else if (rangeSpeed.value >= 80 && rangeSpeed.value < 100)
    {
        animFanB.style.animationDuration = "1s"
        animFanW.style.animationDuration = "1s"
    }
    else if (rangeSpeed.value == 0)
    {
        animFanB.style.animationDuration = "0s"
        animFanW.style.animationDuration = "0s"
    }    
}

switchMode.addEventListener("input", function ()
{
    let theme = document.getElementById("theme")

    if (theme.getAttribute("href") == "/css/day_mode.css")
    {
        theme.href = "/css/night_mode.css" 
    }
    else
    {
        theme.href = "/css/day_mode.css"
    }
});

switchLang.addEventListener("input", function ()
{
    if (document.getElementById("lang_fan").innerHTML == languages["fan speed"].en)
    {
        document.getElementById("lang_fan").innerHTML = languages["fan speed"].ru
        document.getElementById("lang_temp").innerHTML = languages.temperature.ru
        document.getElementById("lang_bar").innerHTML = languages.bar.ru
        document.getElementById("lang_hygr").innerHTML = languages.hygr.ru
    }
    else if (document.getElementById("lang_fan").innerHTML == languages["fan speed"].ru)
    {
        document.getElementById("lang_fan").innerHTML = languages["fan speed"].en
        document.getElementById("lang_temp").innerHTML = languages.temperature.en
        document.getElementById("lang_bar").innerHTML = languages.bar.en
        document.getElementById("lang_hygr").innerHTML = languages.hygr.en
    }
});


speed.addEventListener("input", function ()
{
    rangeSpeed.value = this.value;
    rangeSpeed.innerHTML = rangeSpeed.value;
    data.setAttribute('data-percent', rangeSpeed.value)
    animateFans();
    progressViewSpeed();
});

function progressViewSpeed()
{
    let diagramBox = document.querySelectorAll('.diagram.progress');
    diagramBox.forEach((box) =>
    {
        let deg = (360 * box.dataset.percent / 100) + 180;
        if (box.dataset.percent >= 50)
        {
            box.classList.add('over_50');
        }
        else
        {
            box.classList.remove('over_50');
        }
        box.querySelector('.piece.right').style.transform = 'rotate('+deg+'deg)';
    });
}
function progressViewHygr()
{
    let diagramBox1 = document.querySelectorAll('.diagram1.progress');
    diagramBox1.forEach((box) =>
    {
        let deg = (360 * box.dataset.percent / 100) + 180;
        if (box.dataset.percent >= 50)
        {
            box.classList.add('over_50');
        }
        else
        {
            box.classList.remove('over_50');
        }
        box.querySelector('.piece.right1').style.transform = 'rotate('+deg+'deg)';
    });
}


const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("/fan")
            .build();

hubConnection.on("ReceiverDataFromServer", function (myObj)
{
    tempValueC.innerHTML = myObj.termValueC.toFixed(2);
    tempValueF.innerHTML = myObj.termValueF.toFixed(2);
    barValueMHg.innerHTML = myObj.barValueMGH;
    barValuePascal.innerHTML = myObj.barValuePascal;
    hygrValue.value = myObj.hygrValue;
    hygrValue.innerHTML = hygrValue.value;
    dataHygrValue.setAttribute('data-percent', hygrValue.value);
    progressViewHygr();            
});

hubConnection.on("StartSpeed", function (myObj)
{
    let value = myObj.speed;
    rangeSpeed.value = value;
    rangeSpeed.innerHTML = rangeSpeed.value;
    data.setAttribute('data-percent', rangeSpeed.value);
    speed.value = value;
});

hubConnection.on("Shutdown", function (result)
{
    if (result)
    {
        alert("Пожалуйста, закройте вкладку.");
    }
    else
    {
        alert("Не удалось выключить устройство.");                
    }
});
                    
turnOff.addEventListener ("click", function()
{
    hubConnection.invoke("Shutdown");
})

speed.addEventListener("input", function ()
{
    rangeSpeed.value = this.value;
    hubConnection.invoke("ReceiveDataFromClient",
        {
            "currentSpeed": parseInt(rangeSpeed.value, 10)
        });
});
hubConnection.start();   
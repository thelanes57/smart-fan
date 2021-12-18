const language = document.getElementById("lang")
const mode = document.getElementById("mode")
const speedFan = document.getElementById("speedFan")
const rangeSpeed = document.getElementById("range_value")
const data = document.querySelector(".diagram.progress");
const temperCe = document.getElementById("temperature_ce");
const temperFe = document.getElementById("temperature_fe");
const barValueHg = document.getElementById("hg");
const barValuePa = document.getElementById("pa");
const hygr = document.getElementById("range_value1");

speedFan.addEventListener("input", function() {
    rangeSpeed.value = this.value;
    rangeSpeed.innerHTML = rangeSpeed.value;
    data.setAttribute('data-percent', rangeSpeed.value)
  });


const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("/fan")
            .build();

        hubConnection.on("Recever", function (myObj){
            temperCe.innerHTML = myObj.tarmValueC;
            temperFe.innerHTML = myObj.tarmValueF;
            barValueHg.innerHTML = myObj.barValueMGH;
            barValuePa.innerHTML = myObj.barValuePascal;
            hygr.innerHTML = myObj.gigValue;
        });

        speedFan.addEventListener("input", function () {
            rangeSpeed.value = this.value;
            hubConnection.invoke("ReciveData", rangeSpeed.value);
        });
        hubConnection.start();





 

    
//function progressView(){
//    let diagramBox = 'data-percent';
//    diagramBox.forEach((box) => {
//        let deg = (360 * box.dataset.percent / 100) + 180;
//        if(box.dataset.percent >= 50){
//            box.classList.add('over_50');
//        }else{
//            box.classList.remove('over_50');
//        }
//        box.querySelector('.piece.right').style.transform = 'rotate('+deg+'deg)';
//    });
//}
//progressView();

// function timer(speed){
//     let diagramBox = document.querySelector('.diagram.progress');
//     speed = rangeSpeed.value;

//     let deg = (360 * speed / rangeSpeed.value) + 180;
//     if(speed >= rangeSpeed.value / 2){
//         diagramBox.classList.add('over_50');
//     }else{
//         diagramBox.classList.remove('over_50');
//     }

//     diagramBox.querySelector('.piece.right').style.transform = 'rotate('+deg+'deg)';
// }
// timer();
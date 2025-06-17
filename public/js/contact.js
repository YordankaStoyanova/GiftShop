const form = document.getElementById('contactForm');
form.addEventListener('submit',()=>{
const name = document.getElementById('name');
const name = document.getElementById('name');
const name = document.getElementById('name');
const name = document.getElementById('name');
fetch('ip/weather/GetWeatherForecast',{
    headers:{
        METHOD:"POST"
    },
    body:JSON.stringify({
        name:name.value
    })
})
});
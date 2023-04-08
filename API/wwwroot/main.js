  function Login(){
    
      let login = document.getElementById("login");
      let password = document.getElementById("password");
      

      const xhr = new XMLHttpRequest();
      xhr.open("POST", "Authentication\\LoginUser");
      xhr.setRequestHeader("Content-Type", "application/json; charset=UTF-8")

      const body = JSON.stringify({
          login: login.value,
          password: password.value,
      });
      
      xhr.onload = () => {
          if (xhr.readyState === 4 && xhr.status === 200) {
              const response = xhr.responseText;

              localStorage.setItem("token", response);
              window.location.href = 'mainpage.html';

          } else {
              console.log(`Error: ${xhr.status}`);
              let alert = document.getElementById("alert");
              alert.classList.remove("d-none");
              alert.innerText = xhr.statusText;
          }
      };
      xhr.send(body);
    return false;
}

function Register() {
    let login = document.getElementById("login");
    let name = document.getElementById("name");
    let secondName = document.getElementById("secondName");
    let password = document.getElementById("password");

    const xhr = new XMLHttpRequest();
    xhr.open("POST", "Authentication\\RegisterUser");
    xhr.setRequestHeader("Content-Type", "application/json; charset=UTF-8")

    const body = JSON.stringify({
        login: login.value,
        name: name.value,
        secondName: secondName.value,
        password: password.value,
    });

    xhr.onload = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
            const response = xhr.responseText;

            localStorage.setItem("token", response);
            window.location.href = 'mainpage.html';

        } else {
            console.log(`Error: ${xhr.status}`);
            let alert = document.getElementById("alert");
            alert.classList.remove("d-none");
            alert.innerText = xhr.statusText;
        }
    };
    xhr.send(body);
    return false;
}

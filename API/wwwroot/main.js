  function Login(){
    
      let login = document.getElementById("login");
      let password = document.getElementById("password");
      

      const xhr = new XMLHttpRequest();
      xhr.open("POST", "Users\\RegisterUser");
      xhr.setRequestHeader("Content-Type", "application/json; charset=UTF-8")

      const body = JSON.stringify({
          login: login.value,
          password: password.value,
      });
      
      xhr.onload = () => {
          if (xhr.readyState === 4 && xhr.status === 201) {
              const response = JSON.parse(xhr.responseText);
              const token = response.token;

              localStorage.setItem("token", token);

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

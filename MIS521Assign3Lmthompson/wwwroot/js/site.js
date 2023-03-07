
async function populate(random) {
    try {
        await fetch('https://mis521assignment3lmthompson.azurewebsites.net/Movies/GetMovieTitle/' + random).then(response => response.text()).then(text =>
            document.getElementById("movieTitle").innerText = text);
    }
    catch (e){
        fetch('https://localhost:7046/Movies/GetMovieTitle/' + random).then(response => response.text()).then(text =>
            document.getElementById("movieTitle").innerText = text);
    }

    try {
        await fetch('https://mis521assignment3lmthompson.azurewebsites.net/Movies/GetMovieYear/' + random).then(response => response.text()).then(text =>
            document.getElementById("movieRelease").innerText = text);
    }
    catch {
    fetch('https://localhost:7046/Movies/GetMovieYear/' + random).then(response => response.text()).then(text =>
        document.getElementById("movieRelease").innerText = text);
    }


}


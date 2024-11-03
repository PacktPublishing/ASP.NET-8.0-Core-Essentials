

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/streamHub")
    .build();

connection.start().then(function() {
    connection.stream("Countdown", 10).subscribe({

        next: (count) => {

            logStream(count);

        },

        complete: () => {

            logStream("Stream completed");

        },

        error: (err) => {

            logStream(err);

        }

    });
}).catch(err => logStream(err.toString()));

function logStream(status) {

    let li = document.createElement("li");

    let ul = document.getElementById("ulLog");

    li.textContent = status;

    ul.appendChild(li);

} 
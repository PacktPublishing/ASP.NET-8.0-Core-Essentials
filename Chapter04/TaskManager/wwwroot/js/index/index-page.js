var HUB_URL = "/taskmanagerhub";
var NOTIFY_TASK_MANAGER_EVENT = "TaskManagerEvent";
var HUB_ADD_TASK_METHOD = "CreateTask";
var HUB_COMPLETE_TASK_METHOD = "CompleteTask";
var TASK_NAME_ID = "taskName";
var ID_PREFIX = "un-";

var connection = new signalR.HubConnectionBuilder().withUrl(HUB_URL).build();

connection.on(NOTIFY_TASK_MANAGER_EVENT, updateTaskList);

connection.start().then(function () {

    addTaskButton.disabled = false;

}).catch(function (err) {

    return console.error(err.toString());

});

function updateTaskList(taskModel) {

    if (taskModel) {

        let taskItem = document.createElement("li");
        taskItem.className = "list-group-item";

        if (taskModel.isCompleted) {
            let taskListCompleted = document.getElementById("completedTaskList");
            
            taskItem.className = "list-group-item d-flex justify-content-between align-items-start";

            var divAlign = document.createElement("div");
            divAlign.className = "ms-2 me-auto";
            divAlign.textContent = taskModel.name;
            taskItem.appendChild(divAlign);
            
            var label = document.createElement("span");
            label.className = "badge text-bg-primary rounded-pill";
            label.textContent = "Completed";
            taskItem.appendChild(label);

            taskListCompleted.appendChild(taskItem);
            

        } else {
            let taskListUnCompleted = document.getElementById("uncompletedTaskList");

            taskItem.id = ID_PREFIX + taskModel.id;
            let span = document.createElement("span");
            span.textContent = taskModel.name + ' - ';
            taskItem.appendChild(span);

            let link = document.createElement("a");
            link.href = "#";
            link.attributes["data-task-id"] = taskModel.id;
            link.attributes["data-task-name"] = taskModel.name;
            link.textContent = "Complete";
            link.className = "btn btn-info";
            link.addEventListener("click", function (event) {

                let taskName = this.attributes["data-task-name"];
                let taskId = this.attributes["data-task-id"];

                connection.invoke(HUB_COMPLETE_TASK_METHOD, { id: taskId, name: taskName }).then(() => {
                    let taskListUnC = document.getElementById("uncompletedTaskList");

                    // Removing the item from the uncompleted list
                    let itemToRemove = document.getElementById(ID_PREFIX + taskId);
                    taskListUnC.removeChild(itemToRemove);


                }).catch(function (err) {

                    return console.error(err.toString());

                });


                event.preventDefault();

            });

            taskItem.appendChild(link);

            taskListUnCompleted.appendChild(taskItem);
        }

    }

}

var addTaskButton = document.getElementById("addTaskButton");

addTaskButton.addEventListener("click", function (event) {

    let taskName = document.getElementById(TASK_NAME_ID);

    connection.invoke(HUB_ADD_TASK_METHOD, { name: taskName.value }).catch(function (err) {

        return console.error(err.toString());

    });

    taskName.value = "";

    taskName.focus();

    event.preventDefault();

});
document.addEventListener("DOMContentLoaded", function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .withAutomaticReconnect()
        .build();

    connection.on("ReceiveMessage", function (user, message) {
        const msgItem = document.createElement("div");
        msgItem.classList.add("message-item");

        const userName = document.createElement("strong");
        userName.textContent = `${user}: `;

        const msgContent = document.createElement("span");
        msgContent.textContent = message;

        msgItem.appendChild(userName);
        msgItem.appendChild(msgContent);

        document.getElementById("messagesList").appendChild(msgItem);
        scrollToBottom();
    });

    function scrollToBottom() {
        var messagesList = document.getElementById("messagesList");
        messagesList.scrollTop = messagesList.scrollHeight;
    }

    connection.start().then(function () {
        const conversationId = document.getElementById("conversationId").value;
        if (conversationId) {
            connection.invoke("JoinConversation", parseInt(conversationId))
                .catch(err => console.error(`Failed to invoke 'JoinConversation': ${err}`));
        } else {
            console.error("Conversation ID not found");
        }
    }).catch(err => console.error(`Connection start failed: ${err}`));

    document.getElementById("sendButton").addEventListener("click", function (event) {
        const conversationId = parseInt(document.getElementById("conversationId").value);
        const userId = parseInt(document.getElementById("userId").value);
        const message = document.getElementById("messageInput").value.trim();

        if (conversationId && userId && message) {
            connection.invoke("SendMessage", conversationId, userId, message)
                .then(() => {
                    // Clear the message input after the message is sent
                    document.getElementById("messageInput").value = '';
                })
                .catch(err => console.error(`Failed to send message: ${err}`));
        } else {
            console.error("Missing conversation ID, user ID, or message");
        }
        event.preventDefault();
    });

});

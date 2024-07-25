document.addEventListener("DOMContentLoaded", function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .withAutomaticReconnect()
        .build();

    connection.on("ReceiveMessage", function (user, message) {
        const noMessageElement = document.getElementById("noMessage");
        if (noMessageElement) {
            noMessageElement.remove();
        }
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

    connection.on("NewConversation", function (conversationId, otherUserName, lastMessageDate) {
        const noConversationElement = document.getElementById("noConversation");
        if (noConversationElement) {
            noConversationElement.remove();
        }
        const conversationList = document.querySelector(".conversation-list ul");
        const newConversationItem = document.createElement("li");
        newConversationItem.classList.add("conversation-item");
        newConversationItem.innerHTML = `
            <img src="/img/undraw_profile_1.svg" alt="User Image" />
            <a href="/ChatPages/ChatPage?handler=Conversation&conversationId=${conversationId}">
                <div>
                    <strong>${otherUserName}</strong><br/>
                    <small>${formatRelativeDate(new Date(lastMessageDate))}</small><br />
                    New conversation
                </div>
            </a>
        `;
        conversationList.insertBefore(newConversationItem, conversationList.firstChild);
    });

    connection.start().then(function () {
        const conversationId = document.getElementById("conversationId").value;
        const userId = document.getElementById("userId").value;
        const isNew = document.getElementById("isNew").value === "true";
        if (conversationId && userId) {
            connection.invoke("JoinConversation", parseInt(conversationId), parseInt(userId), isNew)
                .catch(err => console.error(`Failed to invoke 'JoinConversation': ${err}`));
        } else {
            console.error("Conversation ID or User ID not found");
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

    function formatRelativeDate(date) {
        const now = new Date();
        const diffInMinutes = Math.floor((now - date) / (1000 * 60));
        if (diffInMinutes < 60) return `${diffInMinutes}m`;
        const diffInHours = Math.floor(diffInMinutes / 60);
        if (diffInHours < 24) return `${diffInHours}h`;
        const diffInDays = Math.floor(diffInHours / 24);
        if (diffInDays < 7) return `${diffInDays}d`;
        return `${Math.floor(diffInDays / 7)}w`;
    }

    function scrollToBottom() {
        var messagesList = document.getElementById("messagesList");
        messagesList.scrollTop = messagesList.scrollHeight;
    }
});

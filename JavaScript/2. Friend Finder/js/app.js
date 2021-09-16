// Your code here...

// Declaration
let FRIENDS_URL = "/friends/friends.json";
const menu = document.getElementById("menu");
const menuHome = document.querySelector(".home");
const menuFriend = document.querySelector(".friends")
const mainContent = document.querySelector(".content");

// Menu home Event Listener: back to home
menuHome.addEventListener("click", () => {
    mainContent.innerHTML = "";
    menuFriend.classList.remove("pure-menu-selected");
})

// Menu Friends Event Listener: to display friends list
menuFriend.addEventListener("click", (event) => {

    event.preventDefault();
    // To get friends data from json file
    fetch(FRIENDS_URL)
        .then(response => response.json())
        .then(friendsData => {
            // Refresh friends list page
            // --- to check if the main content is not empty
            // --- then empty the page
            // --- then add friends list
            if (mainContent != "") {
                mainContent.innerHTML = "";
                createContentFriendsListElement(friendsData);
            }
        }).catch((err) => {
            console.log(err);
        })
    // To change menu friend backgroundcolor after click
    menuFriend.classList.add("pure-menu-selected");
})

// To build menu friends list fragment
function createContentFriendsListElement(friendsData) {
    // create div
    const mainContentDiv = document.createElement("div");
    mainContentDiv.setAttribute = ("class", "pure-menu custom-restricted-width");
    mainContent.appendChild(mainContentDiv);
    // create span
    const mainContentSpan = document.createElement("span");
    const mainContentSpanTxt = document.createTextNode("Friends");
    mainContentSpan.setAttribute("class", "pure-menu-heading");
    mainContentDiv.appendChild(mainContentSpan);
    mainContentSpan.appendChild(mainContentSpanTxt);
    // create ul
    const mainContentUl = document.createElement("ul");
    mainContentUl.setAttribute("class", "pure-menu-list");
    mainContentDiv.appendChild(mainContentUl); // ul    

    // Create friends link: li and a
    friendsData.forEach(friends => {
        // To load each object in an Array
        const friendList = document.querySelector(".content ul");
        // Li for friends list
        friendList.innerHTML += `<li class="pure-menu-item">
        <a href="#" class="pure-menu-link" data-id=${friends.id}>${friends.firstName} ${friends.lastName}</a>
        </li>`;
    })
}

// Individual Event Listener: to display individual information
mainContent.addEventListener("click", (event) => {
    let target = event.target;

    // To get friends data
    fetch(FRIENDS_URL)
        .then(response => response.json())
        .then(friendsData => {
            // To get each object from array
            for (let i = 0; i < friendsData.length; i++) {
                var id = friendsData[i].id;
            }
            // To chekc if clicked indiviual's id 
            // --- is equal to the id from freindsData json file
            // --- then get the indiviudal data json file
            if (id = target.getAttribute("data-id")) {
                fetch(`/friends/${id}.json`)
                    .then(response => response.json())
                    .then(individual => {
                        mainContent.innerHTML = "";
                        createIndividualFriendElement(individual.firstName, individual.lastName, individual.avatar, individual.email, individual.hometown, individual.bio);
                    }).catch((err) => {
                        console.log(err);
                    })
            }
        })
})

// to build individul fragment
function createIndividualFriendElement(firstName, lastName, image, email, hometown, bio) {
    mainContent.innerHTML += `<div class="friend">
        <div class="identity">
            <img src="/img/${image}" class="photo" />
            <h2 class="name">${firstName} ${lastName}</h2>
            <ul>
                <li><span class="label">email:</span> ${email}</li>
                <li><span class="label">hometown:</span> ${hometown}</li>
            </ul> 
        <p class="bio">${bio}</p>
        </div>
    </div>`
}

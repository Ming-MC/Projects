// Your code here...

// Declaration
let FRIENDS_URL = "/friends/friends.json";
const menu = document.getElementById("menu");
const menuHome = document.querySelector(".home");
const menuFriend = document.querySelector(".friends")
const mainContent = document.querySelector(".content");

// upate json file
function updateJsonFile(individual, id) {
    if (id == "1") {
        individual["email"] = "janed@gmail.com";
        individual["hometown"] = "Edmonton, AB";
        individual["bio"] = "Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum semper orci eu nibh molestie feugiat. Integer mollis rhoncus nisl at condimentum. Vestibulum aliquet et magna mollis tempor. Vivamus urna augue, luctus sit amet lorem non, porttitor elementum enim. Proin volutpat odio sed magna cursus, sit amet tincidunt lectus congue. Praesent scelerisque volutpat purus sit amet efficitur.";
    } else if (id == "2") {
        individual["email"] = "tom.jones@yahoo.com";
        individual["hometown"] = "Calgary, AB";
        individual["bio"] = "Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum semper orci eu nibh molestie feugiat. Integer mollis rhoncus nisl at condimentum. Vestibulum aliquet et magna mollis tempor. Vivamus urna augue, luctus sit amet lorem non, porttitor elementum enim. Proin volutpat odio sed magna cursus, sit amet tincidunt lectus congue. Praesent scelerisque volutpat purus sit amet efficitur.";
    } else if (id == "3") {
        individual["email"] = "clark_kent@nait.com";
        individual["hometown"] = "Toronto, On";
        individual["bio"] = "Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum semper orci eu nibh molestie feugiat. Integer mollis rhoncus nisl at condimentum. Vestibulum aliquet et magna mollis tempor. Vivamus urna augue, luctus sit amet lorem non, porttitor elementum enim. Proin volutpat odio sed magna cursus, sit amet tincidunt lectus congue. Praesent scelerisque volutpat purus sit amet efficitur.";
    } else if (id == "4") {
        individual["email"] = "sally.anne@nait.com";
        individual["hometown"] = "Edmonton, AB";
        individual["bio"] = "Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum semper orci eu nibh molestie feugiat. Integer mollis rhoncus nisl at condimentum. Vestibulum aliquet et magna mollis tempor. Vivamus urna augue, luctus sit amet lorem non, porttitor elementum enim. Proin volutpat odio sed magna cursus, sit amet tincidunt lectus congue. Praesent scelerisque volutpat purus sit amet efficitur.";
    }
}

// Set Menu home 
menuHome.addEventListener("click", () => {
    mainContent.innerHTML = "";
    menuFriend.classList.remove("pure-menu-selected");
})

// Set Menu friend
menuFriend.addEventListener("click", () => {
    // To check if main content is blank 
    if (mainContent.textContent == "" || mainContent.textContent == null) {
        // To build main content friends list fragment
        createContentFriendsListElement();
        // To get friends data from json file
        fetch(FRIENDS_URL)
            .then(response => response.json())
            .then(friendsData => {
                // To load each object in an Array
                friendsData.forEach(friends => {
                    const friendList = document.querySelector(".content ul");
                    // Li for friends list
                    friendList.innerHTML += `<li class="pure-menu-item">
                    <a href="#" class="pure-menu-link" data-id=${friends.id}>${friends.firstName} ${friends.lastName}</a>
                    </li>`
                })
            }).catch((err) => {
                console.log(err);
            })
    } else {
        location.reload();
    }
    // To change menu friend backgroundcolor after click
    menuFriend.classList.add("pure-menu-selected");

});

// To build menu friends list fragment
function createContentFriendsListElement() {
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
}

mainContent.addEventListener("click", (event) => {
    let target = event.target;
    // To fetch friends data from json file
    fetch(FRIENDS_URL)
        .then(response => response.json())
        .then(friendsData => {
            // Get friendsData Object
            for (let i = 0; i < friendsData.length; i++) {
                var id = friendsData[i].id;
                var img = friendsData[i].img;
            }
            // To check if id from json file is equal to friend list link id
            if (id = target.getAttribute("data-id")) {
                // To fetch data from individual json file
                fetch(`/friends/${id}.json`)
                    .then(response => response.json())
                    .then(individual => {
                        // Calling funciton to update individual info 
                        updateJsonFile(individual, individual.id);
                        mainContent.innerHTML = "";
                        // to get fragment for individual friend
                        createIndividualFriendElement(individual.firstName, individual.lastName, individual.avatar);
                        // Declare Identity div
                        const individualIdentityDiv = document.querySelector(".identity");
                        // Declare ul
                        const individualListUl = document.querySelector(".identity ul");
                        // Li for email
                        individualListUl.innerHTML += `<li>
                        <span class="label">Email:</span> ${individual.email}<li>`;
                        // li for hometown
                        individualListUl.innerHTML += `<li>
                        <span class="label">Hometown:</span> ${individual.hometown}<li>`;
                        // p for bio
                        individualIdentityDiv.innerHTML += `<p class="bio">${individual.bio}</p>`
                    }).catch((err) => {
                        console.log(err);
                    })
            }
        }).catch((err) => {
            console.log(err);
        })
});

// To build individual fragment
function createIndividualFriendElement(firstName, lastName, image) {
    // create div: friend
    let individualFriendDiv = document.createElement("div");
    individualFriendDiv.setAttribute("class", "friend");
    mainContent.appendChild(individualFriendDiv);
    // create div: identity
    let individualIdentityDiv = document.createElement("div");
    individualIdentityDiv.setAttribute("class", "identity");
    individualFriendDiv.appendChild(individualIdentityDiv);
    // create img
    let individualImg = document.createElement("img");
    individualIdentityDiv.appendChild(individualImg);
    individualImg.setAttribute("src", `/img/${image}`);
    individualImg.setAttribute("class", "photo");
    // create h2
    let individulH2 = document.createElement("h2");
    individulH2.setAttribute("class", "name");
    individualIdentityDiv.appendChild(individulH2);
    let individulH2Txt = document.createTextNode(firstName + " " + lastName);
    individulH2.appendChild(individulH2Txt);
    // create ul
    let individualUl = document.createElement("ul");
    individualIdentityDiv.appendChild(individualUl);
}

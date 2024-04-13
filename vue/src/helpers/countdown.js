export function reformatDate(expiration) {
    var date = new Date(Date.parse(expiration));
    const now = Date.now();
    let timeRemaining = date - now;

    var days = Math.floor(timeRemaining / (1000 * 60 * 60 * 24));
    var hours = Math.floor((timeRemaining % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((timeRemaining % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((timeRemaining % (1000 * 60)) / 1000);
    console.log(typeof timeRemaining);
    if (timeRemaining < 0) {
        return "EXPIRED"
    }
    else {
    return `${days} D ${hours} H ${minutes} M ${seconds} S `;
    }
}
(() => {
    if (!localStorage.hasOwnProperty('sessionUser')) {
        document.location.href = 'signup.html';
    }
})();
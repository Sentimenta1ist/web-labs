import FormReader from '../utils/FormReader.js';
import LUser from '../model/LUser.js';

const form = document.querySelector('form');

form.addEventListener('submit', (event) => {
    event.preventDefault();

    const data = FormReader.getData(form);
    
    if (!data.email.length) {
        alert('Wrong email');
    } else {
        const user = LUser.findByEmail(data.email);
        if ((user == undefined)) {
            alert('There is no user');
        } else {
            if (user.password === data.password) {
                localStorage.setItem('sessionUser', JSON.stringify(user));
                document.location.href = '/profile.html';
            } else {
                alert('Wrong password');
            }
        }
    }
});
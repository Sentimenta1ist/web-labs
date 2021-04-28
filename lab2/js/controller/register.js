import { User } from '../model/User.js';
import FormReader from '../utils/FormReader.js';

const form = document.querySelector('form');

form.addEventListener('submit', (event) => {
    event.preventDefault();

    const data = FormReader.getData(form);

    if (/\d|\s/.test(data.firstName) || data.firstName.length < 3) {
        alert('Wrong first name');
    } else if (/\d|\s/.test(data.lastName) || data.lastName.length < 3) {
        alert('Wrong last name');
    } else if (/\s/.test(data.nickname) || data.nickname.length < 3) {
        alert('Wrong nickname');
    } else if (/\s/.test(data.password) || !data.password.length) {
        alert('Wrong password');
    } else {
        new User({ ...data }).save();
        form.reset();
        document.location.href = '/signup.html';
    }
});
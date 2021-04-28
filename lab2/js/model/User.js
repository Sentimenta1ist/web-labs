export class User {
    constructor({ firstName, lastName, nickname, email, password }) {
        this.id = Math.round(Math.random() * 100000).toString();
        this.firstName = firstName;
        this.lastName = lastName;
        this.nickname = nickname;
        this.email = email;
        this.password = password;
    }

    save() {
        if (!localStorage.hasOwnProperty('users')) {
            const users = [];

            users.push({ ...this });
            localStorage.setItem('users', JSON.stringify(users));
        } else {
            const users = JSON.parse(localStorage.getItem('users'));

            users.push({ ...this });
            localStorage.setItem('users', JSON.stringify(users));
        }
    }
}
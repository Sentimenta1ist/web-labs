export class Profile {
    constructor({ firstName, lastName, email, password }) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
    }
    
    render(tableBody) {
        const element = document.createElement('tr');

        element.innerHTML = `
            <th scope="row">${this.firstName}</th>
            <td>${this.lastName}</td>
            <td>${this.email}</td>
            <td>${this.password}</td>
        `;

        tableBody.append(element);
    }
}
class LSUser {
    constructor(users) {
        this.users = !users ? [] : users;
    }
    
    findByEmail(email) {
        const idx = this.users.findIndex(item => item.email === email);

        if (idx < 0) {
            return {};
        }

        return this.users[idx];
    }
}

export default new LSUser(JSON.parse(localStorage.getItem('users')));
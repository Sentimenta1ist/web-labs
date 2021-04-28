class FormReader {
    getData(form) {
        return Object.fromEntries(new FormData(form));
    }
}

export default new FormReader();
export default {
    baseApiUrl: "https://localhost:44377/api/",
    baseUrl: "https://localhost:44377/",
    constructUrl(path) {
        return this.baseApiUrl + path
    }
}
export class User {
    constructor(
        public id: number,
        public email: string,
        public password: string,
        public name: string,
        public token: string,
        public position: number
    ) { }
}
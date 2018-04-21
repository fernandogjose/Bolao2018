export class ChatCreateRequest {
    constructor(
        public userId: number,
        public message: string,
    ) { }
}
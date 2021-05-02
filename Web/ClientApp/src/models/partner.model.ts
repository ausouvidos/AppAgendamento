export default class Partner {
    public id: number;
    public name?: string;
    public website?: string;
    public order?: number;

    public get logoUrl() {
        return `/api/Partners/Logo/${this.id}`;
    }

    constructor(obj: Partner) {
        this.id = obj?.id;
        this.name = obj?.name;
        this.website = obj?.website;
        this.order = obj?.order || 0;
    }
}

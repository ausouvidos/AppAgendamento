export default class Partner {
    public id: number;
    public name?: string;
    public website?: string;
    public order?: number;
    public logoId?: string;

    public get logoUrl() {
        return `/api/Partners/Logo/${this.logoId}`;
    }

    constructor(obj: Partner) {
        this.id = obj?.id;
        this.name = obj?.name;
        this.website = obj?.website;
        this.order = obj?.order || 0;
        this.logoId = obj?.logoId || '';
    }
}

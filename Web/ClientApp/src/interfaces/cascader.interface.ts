export interface ICascaderChildren {
    value: number;
    label: string;
}

export interface ICascader {
    value: string;
    label: string;
    children: ICascaderChildren[];
}

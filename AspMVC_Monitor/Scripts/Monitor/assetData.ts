import { Tag } from "./tag";

export interface AssetData {
    id: number;
    name: string;
    ipAddress: string;
    inAlarm: boolean;
    tags: Tag[];
}

import {BaseEntityService} from "@/services/BaseEntityService.ts";
import type {IStockAudit} from "@/domain/logic/IStockAudit.ts";
import type {IStorageRoom} from "@/domain/logic/IStorageRoom.ts";
import type {IResultObject} from "@/types/IResultObject.ts";

export class StorageRoomService extends BaseEntityService<IStorageRoom> {
  constructor() {
    super('storageRooms');
  }

  async getByInventoryId(id: string): Promise<IResultObject<IStorageRoom[]>> {
    const response = await this.axiosInstance.get(`/storagerooms/inventory/${id}`);
    return { data: response.data };
  }
}

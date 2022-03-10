﻿namespace Codility.WarehouseApi;

public interface IWarehouseRepository
{
    void SetCapacityRecord(int productId, int capacity);
    IEnumerable<CapacityRecord> GetCapacityRecords();
    IEnumerable<CapacityRecord> GetCapacityRecords(Func<CapacityRecord, bool> filter);

    void SetProductRecord(int productId, int qty);
    IEnumerable<ProductRecord> GetProductRecords();
    IEnumerable<ProductRecord> GetProductRecords(Func<ProductRecord, bool> filter);

}
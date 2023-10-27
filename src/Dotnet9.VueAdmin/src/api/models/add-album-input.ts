/* tslint:disable */
/* eslint-disable */
/**
 * 博客后端接口
 * Dotnet9后端
 *
 * OpenAPI spec version: 0.0.1
 * Contact: 632871194@qq.com
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { AvailabilityStatus } from './availability-status';
/**
 * 
 * @export
 * @interface AddAlbumInput
 */
export interface AddAlbumInput {
    /**
     * 专辑名称
     * @type {string}
     * @memberof AddAlbumInput
     */
    name: string;
    /**
     * 专辑别名
     * @type {string}
     * @memberof AddAlbumInput
     */
    slug: string;
    /**
     * 封面图
     * @type {string}
     * @memberof AddAlbumInput
     */
    cover: string;
    /**
     * 
     * @type {AvailabilityStatus}
     * @memberof AddAlbumInput
     */
    status?: AvailabilityStatus;
    /**
     * 排序值（值越小越靠前）
     * @type {number}
     * @memberof AddAlbumInput
     */
    sort: number;
    /**
     * 备注
     * @type {string}
     * @memberof AddAlbumInput
     */
    remark?: string | null;
}

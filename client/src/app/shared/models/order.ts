import { IAddress } from "./address"

export interface IOrderToCreate {
  basketId: string
  deliveryMethodId: number
  shipToAddress: IAddress
}

export interface IOrder {
  buyerEmail: string
  orderDate: string
  shipToAddress: IAddress
  deliveryMethod: string
  status: string
  items: IOrderItem[]
  paymentIntentId: string
  shippingPrice: number
  subTotal: number
  total: number
}

export interface IOrderItem {
  productId: number
  productName: string
  pictureUrl: string
  price: number
  quantity: number
}

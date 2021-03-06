---
title: DataBus Feature
summary: DataBus feature
tags:
- DataBus
- Attachments
redirects:
- nservicebus/databus
related:
- samples/databus
---

## What is the DataBus feature for

Messages are intended to be small. Some scenarios require sending large binary data along with a message. For this purpose, NServiceBus has a DataBus feature to allow you to overcome the message size limitations imposed by underlying transport.

## How it works

The `DataBus` approach is to store a large payload in a location that both the sending and receiving parties can access. The message is sent with a reference to the location, and upon processing, the payload is brought, allowing the receiving part to access the message along with the payload. If the location is not available upon sending, the message fails the send operation. When the payload location is not available, the receive operation fails as well and results in standard NServiceBus behavior, causing retries and eventually going into the error queue.

## How to enable DataBus

NServiceBus supports two DataBus implementations:

* `FileShareDataBus`
* `AzureDataBus`

To enable DataBus, NServiceBus needs to be configured. For file share based DataBus:
<!-- import FileShareDataBus -->

For Azure (storage blobs) based DataBus:
<!-- import AzureDataBus -->

NOTE: The `AzureDataBus` implementation is part of the Azure transport package.

## Specifying message properties for DataBus

There are two ways to specify the message properties to be sent using DataBus
1. Using `DataBusProperty<T>` type
2. Message conventions

### Using DataBusProperty<T>

Properties defined using the `DataBusProperty<T>` type provided by NServiceBus are not treated as part of a message, but persist externally based on the type of `DataBus` used, and are linked to the original message using a unique key. 

<!-- import MessageWithLargePayload -->

### Using message conventions

NServiceBus supports defining DataBus properties via convention. This allows defining a convention for data properties to be sent using `DataBus` without using `DataBusProperty<T>`.

<!-- import DefineMessageWithLargePayloadUsingConvention -->

<!-- import MessageWithLargePayloadUsingConvention -->

##DataBus attachments cleanup

NServiceBus `DataBus` implementations currently behave differently with regard to cleanup of physical attachments used to transfer data properties. `FileShareDataBus` **does not** remove physical attachments once the message is gone. `AzureDataBus` **does** remove Azure storage blobs used for physical attachments once the message is gone.

## Configuring AzureDataBus

The following extension methods are available for changing the behavior of `AzureDataBus` defaults:

<!-- import AzureDataBusConfiguration -->

- `ConnectionString()`: the connection string to the storage account for storing DataBus properties, defaults to `UseDevelopmentStorage=true`
- `Container()`: container name, defaults to '`databus`'
- `BasePath()`: the blobs base path under the container, defaults to empty string
- `DefaultTTL`: time in seconds to keep blob in storage before it is removed, defaults to `Int64.MaxValue` seconds
- `MaxRetries`: number of upload/download retries, defaults to 5 retries
- `NumberOfIOThreads`: number of blocks that will be simultaneously uploaded, defaults to 5 threads
- `BackOffInterval`:  the back-off time between retries, defaults to 30 seconds
- `BlockSize`: the size of a single block for upload when the number of IO threads is more than 1, defaults to 4MB

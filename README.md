# TransactionalFilter

## Files of interest

- Backend/LoggingCallFilter.cs
- Backend/MessageGrain.cs

## Steps to reproduce

- Step 1: Clone project
- Step 2: Open project in Visual Studio
- Step 3: Set debugging mode to "Docker" (will not work with anything else)
- Step 4: Start debugging.
- Step 5: When the browser pops up with the localhost:{port-number} add /graphql to the end of the address.
- Step 6: Click Create document and then apply (if a pop up shows up, click accept)
- Step 7: Copy and paste this into the document editing panel on the left hand side:

```
mutation SEND_MESSAGE {
  sendMessage(input: {
    senderId: "1f5413f8-1bb1-4ef5-9f4a-f20935b1d533"
    recipientId: "2083d3b8-771c-4300-96a1-7ff75a5e274e"
    message: "test"
  }) {
    messageModel {
      message
    }
  }
}
```

- Step 8: Hit "Run" button in the top-center.
- Step 9: View issue.

The expected behavior would be that the LoggingFilter would catch the exception after calling invoke on the grain; however, it doesn't.
You can see based upon the console logs that "MessageGrain call invoked" is sent, but the exception is never handled by the filter.

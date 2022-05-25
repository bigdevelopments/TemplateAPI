# Template API - Starting point for a service
This is a very simple and somewhat pointless service based on the .NET6 minimal API approach, offered
as a starting point for real services for fleshing out. Just shows DI bootstrapping and returns JSON
either as a string or by serialising model objects.

Authentication and Authorisation are supported by passing JWT bearer tokens, passed in in the
Authorization header. A secret key, passed as config as 'TokenKey' is the secret used for signing
and validating the tokens. A seperate little console app shows how a token can be generated and
how to add claims. Stick your roles in the 'role' claim, then use the [Authorize] attibute to
check for roles where needed.

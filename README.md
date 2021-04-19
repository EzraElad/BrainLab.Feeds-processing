# Welcome to the Feed-Processing API!

This API is all about processing request from social media and saves it to your machine in a JSON file
in your desired location

## Startup

Make sure that both web API projects are running together and press the start button

## Usage
You can configure the path that the service saves it to by sending a query parameter with the post request

```bash
https://localhost:44324/api/feeds?configDirectory=<Desired-path>
```

Or using the default path
```bash
https://localhost:44324/api/feeds
```

Send the services a JSON (Can use the example below)

For Twitter -
```python
{
	"id" : "BBA3B514-80EF-491C-956C-406EECD798FE",
	"token" : "eyJhbGciOiJIUzI1NI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2f36POk6yJV_adQssw5c",
	"source"  : "twitter",
	"careated_at": "",
	"tweets" : [
		{	
			"created_at": "01-02-2020",
			"id": 1050118621198921728,
			"text": "click this link to view more details https://www.expressions.com/MkGjXf9aXm",
			"user" : 
			{
				"id":"4534534",
				"name" : "John Doe",
				"location" : "San Francisco, CA",
				"created_at" : "01-01-2000"
			}
		},
		{	
			"created_at": "02-06-2020",
			"id": 4344355698921728,
			"text": "content sdfsdfs fghfgh fjgh",
			"user": {
				"id":"56756756",
				"name" : "Mike michael",
				"location" : "New York",
				"created_at" : "01-02-2000"
			}
		}
	]
}

```

For Facebook - 
```python
{
	"id" : "BBCC112-80EF-491C-956C-406EECD798FE",
	"token" : "sdfsg548e97t5g.eyJzdWIiOiIxMjM0NTF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2f36POk6yJV_adQssw5c",
	"careated_at": "",
	"source" : "facebook",
    "posts": [
		{	
			"create_date": "01-02-2020",
			"id": "sdfsfdsd",
			"subject" : "animals",
			"content": "animals exist in nature",
			"creator" : 
			{
				"id":"4534534",
				"name" : "facebook user",
				"location" : "USA",
				"mail" : "facebook.user@gmail.com"
			}
		}
	]
}

```


## License
Elad Ezra

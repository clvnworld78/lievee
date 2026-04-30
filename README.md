# API Usage Documentation

## Base URL

```txt
http://localhost:5006
```

---

## Usage Flow

1. Restore the Postgres18 database provided in the repo
2. Create admin and user first.
3. Login.
4. Use the provided cookie from login for the routes requiring admin.
5. For registration, use `RegistrationLink` first to get the code.
6. Use that code during `Registration POST`.

---

# Holiday

## Holiday POST

```powershell
curl.exe -X 'POST' 'http://localhost:5006/Holiday' -H 'accept: */*' -H 'Content-Type: application/json' -d '"2026-04-30"'
```

---

## Holiday GET

```powershell
curl.exe -X 'GET' 'http://localhost:5006/Holiday?StartDate=2026-05-01&EndDate=2026-05-02' -H 'accept: */*' -H 'Cookie: token=16cacd42-5827-446c-818b-b5c06c5d5bfd'
```

---

## Holiday PATCH

```powershell
curl.exe -X 'PATCH' 'http://localhost:5006/Holiday' `
    -H 'accept: */*' `
    -H 'Content-Type: application/json' `
    -H 'Cookie: token=16cacd42-5827-446c-818b-b5c06c5d5bfd' `
    -d '{\"holidayId\":3,\"newDate\":\"2026-04-30\"}'
```

---

## Holiday DELETE

```powershell
curl.exe -X 'DELETE' 'http://localhost:5006/Holiday?holidayId=2' -H 'accept: */*' -H 'Cookie: token=16cacd42-5827-446c-818b-b5c06c5d5bfd'
```

---

# Registration

## Registration GET

```powershell
curl.exe -X 'GET' 'http://localhost:5006/Registration' -H 'accept: */*' -H 'Cookie: token=16cacd42-5827-446c-818b-b5c06c5d5bfd'
```

---

## Registration POST

Use `RegistrationLink` first to get the code.

Then use that code during `Registration POST`.

```powershell
curl.exe -X POST 'http://localhost:5006/Registration/33177a83-bfb0-4aec-87da-f6b068a34a4e' `
	-H 'accept: */*' `
	-H 'Content-Type: application/json' `
	-H 'Cookie: token=16cacd42-5827-446c-818b-b5c06c5d5bfd' `
	-d '{\"name\":\"test\",\"phoneNumber\":\"087780980333\",\"visitDate\":\"2026-05-02\"}'
```

---

## Registration DELETE

```powershell
curl.exe -X 'DELETE' 'http://localhost:5006/Registration/2' -H 'accept: */*' -H 'Cookie: token=16cacd42-5827-446c-818b-b5c06c5d5bfd'
```

---

# Registration Link

## RegistrationLink POST

```powershell
curl.exe -X 'POST' 'http://localhost:5006/RegistrationLink' -H 'accept: */*'
```

---

# Login

## Login POST

```powershell
curl.exe -X POST 'http://localhost:5006/login' `
	-H 'accept: */*' `
	-H 'Content-Type: application/json' `
	-d '{\"username\":\"user\",\"password\":\"user\"}'
```

---

# User

## Create Admin

Create admin and user first, then login to use the provided cookie for the routes requiring admin.

```powershell
curl.exe -X 'POST' 'http://localhost:5006/User/admin' `
    -H 'accept: */*' `
    -H 'Content-Type: application/json' `
    -d '{\"username\":\"string\",\"password\":\"string\"}'
```


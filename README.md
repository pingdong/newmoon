### **Summary**<br />
Newmoon is an ongoing repeatly rebuilding project with the latest technology and best practices. The idea of the app is simple, a service provides events booking.

The current version focuses on high scalability and high availability on Azure platform. 

It covers the following concept, technology and practice
1) Infrastructure as Code
2) DevOps
3) Serverless
4) Microservice

Instead of using container, I chose serverless as it contains less moving part and lower TCO.

### **Architect**<br />
<img src="images/architect.png" width="500">

### **Repos**<br />
Shared Resources<br />
[repo](https://github.com/pingdong/newmoon.shared) <br />

Authentication<br />
[repo](https://github.com/pingdong/newmoon.authentication)<br />

Place Service<br />
[repo](https://github.com/pingdong/newmoon.places)<br />

Booking Service<br />
[repo](https://github.com/pingdong/newmoon.bookings)<br />

Note: If You're looking for the old container version, please check the [container_2019](https://github.com/pingdong/newmoon/tree/container_2019) branch.

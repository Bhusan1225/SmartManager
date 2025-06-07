# SMART Manager – Unity Game Prototype

**SMART Manager** is a mobile-first, third-person simulation game where the player takes charge of managing and running a mart. The player manually stocks racks, earns money through customer purchases, and unlocks new features as they progress through levels.

---

## Gameplay Summary

- Control the mart manager using an on-screen joystick.
- Begin at Level 1:
  - Collect products from the storage area.
  - Restock products on store shelves manually.
  - Serve customers who buy products and pay at the counter.
- Advance levels by hitting money targets and unlock:
  - New racks and products
  - A staff assistant with AI support
  - Increased difficulty and store upgrades

---

## Player Controls

| Action                   | Control Method                             |
|--------------------------|---------------------------------------------|
| Move Manager             | On-screen Joystick                         |
| Pick Up Product          | Auto-pick when near storage + tap/click    |
| Place Product on Rack    | Auto-place when near shelf + tap/click     |
| Interact with Counter    | Move manager near the cash counter         |

**Note:** Customers can only pay if the manager is present at the cash counter.

---

## AI and Interactions

- **Manager**: Controlled by the player.
- **Customer**: AI pathfinding to racks and counter; carries 1 product.
- **Staff (Level 2+)**: AI assistant to collect and restock products; carries 2 products.

All AI characters use simple, scalable logic for interaction and navigation.

---

## Game Architecture and Code Design

- **Design Principles**:  
  Follows SOLID principles focusing on Single Responsibility and Open-Closed.

- **Patterns Used**:
  - MVC Architecture for clean separation of data, logic, and UI.
  - Singleton Pattern for global services like scene management and inventory tracking.

---

## Platform

- Unity Engine
- Mobile-first, optimized for touch controls (third-person perspective)

---

## Future Scope

- Multiple store locations
- Vendor systems (buying/selling inventory)
- Timed challenges and customer queues
- Full economic simulation with profit tracking

---

## Project Structure (Sample)
![SmartManagerUserFlow-Page-1 drawio](https://github.com/user-attachments/assets/588cda9b-e3ec-4601-a493-8e300c1015ad)
![SmartManagerUserFlow-UML drawio](https://github.com/user-attachments/assets/3acc783e-dee2-4d13-96e9-ec507669c80e)




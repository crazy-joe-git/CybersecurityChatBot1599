# CybersecurityChatBot1599 (E-Bot)

## Overview
The Cybersecurity Awareness Bot (E-Bot) is a high-fidelity, modern desktop application developed in C# using Windows Presentation Foundation (WPF) and .NET 8. Migrated from a legacy text-based console framework, this intelligent security assistant is designed to train and evaluate users on critical online defense vectors through real-time communication, utility command parsing, and live interactive knowledge evaluations.

E-Bot combines responsive graphic styling with an underlying state machine engine to create an educational, secure training hub.

## Key Features
* **Modern Dark-Mode UI:** A premium, custom-styled graphical chat interface featuring distinct message bubbles (royal purple for users, sleek matte gray for the bot) alongside an administrative historical navigation sidebar.
* **Decoupled Retro ASCII Art:** Retains the classic console heritage banner extracted from `UIHelper.cs`, dynamically aligned and rendered inside the GUI layout using crisp monospace text scaling.
* **Native Audio Engine:** Integrates native, asynchronous playback of a local `greeting.wav` file upon application startup without freezing or introducing latency into the UI render pipeline.
* **Interactive Knowledge Evaluation Matrix:** Implements a multi-tier quiz engine that transitions users smoothly into practical cybersecurity training. To ensure user score safety and prevent input spamming, option choices are instantly locked upon selection until the next question is fetched.
* **Defensive Visual Feedback:** Features clear, instant color indicators during evaluations—flashing **Green** to confirm correct structural answers and **Red** to denote incorrect selections.
* **Persistent Performance Scorecard:** Telemetry tracking engine that logs user answers, tallies passed points, updates total inquiries, and outputs a real-time security readiness rating profile.
* **Terminal Slash Command System:** Intercepts system commands (such as `/help`, `/status`, and `/topics`) to query database matrices and display diagnostic panels directly inside the chat environment.

## Cybersecurity Infrastructure Covered
Users can interact directly with the knowledge database or explore simulations regarding:
* **Password Architecture:** Enforcement of passphrases over single words, token hygiene, and credential reuse vulnerability data.
* **Phishing Audits:** Recognition of urgent terminology, suspicious email link domains, and corporate social engineering tactics.
* **Financial Fraud & Scams:** Identifying phishing delivery alerts, illegitimate tech support operations, and sweepstakes scams.
* **Personal Digital Privacy:** Controlling profile visibility, preventing over-sharing on social networks, and identifying unencrypted (HTTP) connections.

## System Commands Terminal Menu
The chatbot automatically intercepts core administrative instructions starting with a forward slash (`/`):
* `SIMULATE` / `TEST` - Disrupts the idle loop and launches the interactive defensive scenario mode.
* `/help` - Displays the available navigation commands and utility keyword directives.
* `/status` - Prints the user's real-time Agent Security Scorecard, displaying past milestones, inquiries count, and current defensive tier.
* `/topics` - Outputs all security knowledge interfaces natively mapped within E-Bot's memory arrays.

## Technologies Used
* **Languages:** C#
* **Frameworks:** .NET 8.0, Windows Presentation Foundation (WPF), Entity Framework Core (SQLite)
* **Development Environment:** Visual Studio 2022
* **Version Control & DevSecOps:** Git, GitHub, Automated GitHub Actions CI/CD Build Engine Pipeline

---

## Visual Presentation & Interface Evolution

### Part 1: Initial Console Prototype Baseline
The historical command-line execution setup, displaying baseline layout logic validation before migrating to modern desktop containers:

![Console Chat Screenshot](https://github.com/user-attachments/assets/04803552-4838-48af-a783-8d3e3ae7d9af)

### Part 2: Upgraded High-Fidelity WPF GUI Layout
The graphical environment showcasing interactive text bubbles, the legacy ASCII title rendering cleanly via `UIHelper`, and live responses from the performance scorecard matrices:

![E-Bot Modern WPF Interface](Screenshot%202026-05-28%20151130.png)

### Part 3: Interactive Quiz & Local Data Management
The finalized, streamlined interactive interface featuring simple, real-world cybersecurity scenarios, explicit Green/Red feedback styling logic, and a fully decoupled SQLite Task Assistant backend.

![E-Bot Part 3 Interactive Interface](Your_New_Screenshot_Name.png)